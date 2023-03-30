using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Contratos;
//using PortalGrupoAlyne.Services;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Claims;
//using System.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PortalGrupoAlyne.Model.Dtos.Usuarios;
using PortalGrupoAlyne.Infra.Services;

namespace PortalGrupoAlyne.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Usuario user = new Usuario();
        private readonly IConfiguration _configuration;
        // private readonly IUserService _userService;
        private readonly DataContext _context;
        private readonly IMailService _mailService;

        public AuthController(IConfiguration configuration, IMailService mailService, DataContext context)
        {
            _context = context;
            _configuration = configuration;
            _mailService = mailService;
            //  _userService = userService;
        }



        [HttpPost("register")]
        // [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Usuario.Any(u => u.Email == request.Email))
            {
                return BadRequest("Usuario ja existe na base de dados.");
            }
            if (_context.Usuario.Any(u => u.Username == request.Username))
            {
                return BadRequest("Usuario ja existe na base de dados.");
            }

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new Usuario
            {
                Email = request.Email,
                Username = request.Username,
                NomeCompleto = request.NomeCompleto,
                Status = request.Status,
                GrupoId = request.GrupoId,
                Funcao = request.Funcao,
                Telefone = request.Telefone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                // VerificationToken = CreateRandomToken()
            };

            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { data = user.Id,
                grupo = user.GrupoId,
                resposta = "Usuário criado com sucesso!" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserLoginRequest request)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Usuario ou senha incorretos.");
            }


            user.Conectado = true;
            await _context.SaveChangesAsync();

            string token = CreateToken(user);

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

            return Ok(new
            {
                id = user.Id,
                Username = user.Username,
                NomeCompleto = user.NomeCompleto,
                GrupoId = user.GrupoId,
                Status = user.Status,
                Email = user.Email,
                ImagemURL = user.ImagemURL,
                Telefone = user.Telefone,
                logado = user.Conectado,
                token
            });
        }

        [HttpPost("logout/{id}")]
        public async Task<ActionResult> Logout(int id)
        {
            var user = await _context.Usuario.FindAsync(id);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            user.Conectado = false;
            await _context.SaveChangesAsync(); //salva as alterações no banco de dados

            return Ok();
        }


        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            //var newRefreshToken = GenerateRefreshToken();
            //SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email, string baseUrl)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            //  var host = HttpContext.Request.Host;

            var resetPasswordLink = baseUrl + "/pga/redefinir-senha?token=" + user.PasswordResetToken;

            // Cria o corpo do email
            var emailBody = $@"<div><h2>Falta pouco para redefinir sua senha!!!<a href=""{resetPasswordLink}""> Clique aqui para redefinir...</a></h2></br></br>
        </br></br></br></br>
        <h2></h2></br></br>
        <img style=""marginTop:20"" src=""https://grupoalynecosmeticos.com.br/wp-content/uploads/2021/03/grupoalyne.png"" width=""200"">";

            // Envia o email
            var emailSend = new SendMailViewModel
            {
                Emails = new List<string> { user.Email }.ToArray(),
                Subject = "Redefinição de senha - Grupo Alyne",
                Body = emailBody,
                IsHtml = true
            };

            await _mailService.SendMailAsync(emailSend);

            return Ok("Email enviado com sucesso!");
        }
    


        //[HttpPost("forgot-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword(string email)
        //{
        //    var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
        //    if (user == null)
        //    {
        //        return BadRequest("Usuário não encontrado.");
        //    }

        //    user.PasswordResetToken = CreateRandomToken();
        //    user.ResetTokenExpires = DateTime.Now.AddDays(1);
        //    await _context.SaveChangesAsync();

        //    return Ok(user.PasswordResetToken);
        //}

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResettPassword(ResetPasswordRequest request)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            //if (user == null || user.ResetTokenExpires < DateTime.Now)
            //{
            //    return BadRequest("Token inválido.");
            //}

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok(new { resposta="Senha alterada com sucesso.",data=user.Username });
        }

      

        private string CreateToken(Usuario user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                //expires: DateTime.Now.AddSeconds(60),
                expires: DateTime.Now.AddHours(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
