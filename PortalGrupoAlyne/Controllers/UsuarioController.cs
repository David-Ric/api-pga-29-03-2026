using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PortalGrupoAlyne.Extension;
using PortalGrupoAlyne.Extension.Helpers;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos.Usuarios;
using PortalGrupoAlyne.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static System.Net.WebRequestMethods;

namespace PortalGrupoAlyne.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private IUserService _userService;
        private IMapper _mapper;
        //   private readonly string _destino = "Perfil";
        private readonly IWebHostEnvironment _environment;


        public UsuarioController(
                                    IUserService userService,
                                    IMapper mapper,
                                    DataContext context,
                                    IWebHostEnvironment environment

                                  )
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _environment = environment;
        }
       
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Usuario.CountAsync();
            var users = await context.Usuario.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).Include("GrupoUsuario").ToListAsync();
           
            return Ok(new
            {
                total,
                data = users
            });
        }
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFilter([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina,
            [FromQuery] string filter
            )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Usuario
            .AsNoTracking()
                 .Where(e => (e.NomeCompleto.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id).Include("GrupoUsuario")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Usuario
            .AsNoTracking()
                .Where(e => (e.NomeCompleto.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

            
        }
        [HttpGet("filter/status")]
          [AllowAnonymous]
        public async Task<IActionResult> GetAllFilterStatus([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina,
            [FromQuery] string filter
            )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Usuario
            .AsNoTracking()
                 .Where(e => (e.Status.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id).Include("GrupoUsuario")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Usuario
            .AsNoTracking()
                .Where(e => (e.Status.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

        }

        [HttpGet("filter/userName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFilterUserName([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
           [FromQuery] string filter
           )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Usuario
            .AsNoTracking()
                 .Where(e => (e.Username.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id).Include("GrupoUsuario")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Usuario
            .AsNoTracking()
                .Where(e => (e.Username.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

        }



        [HttpGet("userName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllName([FromServices] DataContext context,

          [FromQuery] string name

          )
        {

            var users = await context.Usuario.AsNoTracking().Where(e => (e.Username.ToLower().Contains(name.ToLower())
                                     ))
                         .OrderBy(e => e.Id).ToListAsync();
          
            return Ok(users);
        }

        [HttpGet("imagem/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImagem([FromServices] DataContext context, string username)
        {
            var user = await context.Usuario.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

            if (user?.Imagem == null)
            {
                return NotFound();
            }

            return File(user.Imagem, "image/jpeg");
        }



        [HttpPost("{userName}/imagem")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage([FromServices] DataContext context, string userName, IFormFileCollection files)
        {
            var user = await context.Usuario.FirstOrDefaultAsync(e => e.Username.ToLower() == userName.ToLower());

            if (user == null)
            {
                // handle error
                return NotFound();
            }

            var file = files.FirstOrDefault();

            if (file == null)
            {
                // handle error
                return BadRequest("Nenhum arquivo enviado");
            }

            var imageBytes = await ReadFully(file.OpenReadStream());
            user.Imagem = imageBytes;

            await context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                ImagemURL = $"/api/usuarios/imagem/{user.Username}"
            });
        }

        private async Task<byte[]> ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        [HttpGet("conectados")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuariosConectados()
        {
            var usuariosConectados = await _context.Usuario.OrderBy(u => u.NomeCompleto).ToListAsync();

            if (usuariosConectados == null || !usuariosConectados.Any())
            {
                return NotFound();
            }

            var usuariosDTO = new List<UserDto>();

            foreach (var usuario in usuariosConectados)
            {
                var imagemBase64 = Convert.ToBase64String(usuario.Imagem);
                usuariosDTO.Add(new UserDto
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    NomeCompleto = usuario.NomeCompleto,
                    ImagemURL = usuario.ImagemURL,
                    Conectado = usuario.Conectado,
                    ImagemBase64 = imagemBase64,

                });
            }

            return usuariosDTO;
        }


        [HttpGet("buscaUsuarios")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuariosConectados([FromQuery] string searchTerm)
        {
            var usuariosConectados = _context.Usuario.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                usuariosConectados = usuariosConectados.Where(u =>
                    u.NomeCompleto.Contains(searchTerm) ||
                    u.Username.Contains(searchTerm));
            }

            usuariosConectados = usuariosConectados.OrderBy(u => u.NomeCompleto);

            var usuariosDTO = await usuariosConectados.Select(usuario => new UserDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                NomeCompleto = usuario.NomeCompleto,
                ImagemURL = usuario.ImagemURL,
                Conectado = usuario.Conectado,
                ImagemBase64 = Convert.ToBase64String(usuario.Imagem),
            }).ToListAsync();

            if (usuariosDTO == null || !usuariosDTO.Any())
            {
                return NotFound();
            }

            return usuariosDTO;
        }




        [HttpGet("por-nome/{nome}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuariosPorNome(string nome)
        {
            var usuarios = await _context.Usuario
                .Where(u => u.NomeCompleto.ToLower().Contains(nome.ToLower()))
                .ToListAsync();

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound();
            }

            var usuariosDTO = new List<UserDto>();

            foreach (var usuario in usuarios)
            {
                var imagemBase64 = Convert.ToBase64String(usuario.Imagem);
                usuariosDTO.Add(new UserDto
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    NomeCompleto = usuario.NomeCompleto,
                    ImagemURL = usuario.ImagemURL,
                    Conectado = usuario.Conectado,
                    ImagemBase64 = imagemBase64,
                });
            }

            return usuariosDTO;
        }


        [HttpGet("conectado/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var imagemBase64 = Convert.ToBase64String(usuario.Imagem);

            var usuarioDTO = new UserDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                NomeCompleto = usuario.NomeCompleto,
                ImagemURL = usuario.ImagemURL,
                Conectado = usuario.Conectado,
                ImagemBase64 = imagemBase64,
            };

            return usuarioDTO;
        }





        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _userService.GetById(id);
                if (usuario == null) return NoContent();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest("Usuario não encontrado.");
            }
        }
         [HttpGet("permissao/{id}")]
       
        public async Task<IActionResult> GetPermissaoById(int id)
         {
             try
             {
                 var usuario = await _userService.GetById(id);
                 if (usuario == null) return NoContent();

                 return Ok(usuario);
             }
             catch (Exception ex)
             {
                 return BadRequest("Usuario não encontrado.");
             }
         }
        


        [HttpPut("{id}")]
        [AllowAnonymous]
        public IActionResult Update(int id, UserUpdateResquest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "Usuário atualizado com sucesso" });
        }



        [HttpPost("UploadImage")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> UploadImage([FromQuery] string name)
        {
            try
            {
                var uploadedFile = Request.Form.Files.FirstOrDefault();
                if (uploadedFile != null)
                {
                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                    }

                    var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Username == name);
                    if (user != null)
                    {
                        user.Imagem = imageData;

                        // Build the image URL based on the current request URL.
                        var urlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";
                        var imageUrl = $"{urlBase}/images/{user.Username}.png";

                        // Update the user with the image URL.
                        user.ImagemURL = imageUrl;
                        await _context.SaveChangesAsync();

                        return Ok(imageUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception
            }

            return BadRequest();
        }

        [NonAction]
        private async Task<FileContentResult> GetImagebyUser(string userName)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Username == userName);
            if (user != null && user.Imagem != null && !string.IsNullOrEmpty(user.ImagemURL))
            {
                return new FileContentResult(user.Imagem, "image/png");
            }

            var noImage = System.IO.File.ReadAllBytes("noimage.png");
            return new FileContentResult(noImage, "image/png");
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Usuario>>> Delete(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return BadRequest("Usuário não encontrado");

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuário excluído com sucesso!");
        }

    }
}
