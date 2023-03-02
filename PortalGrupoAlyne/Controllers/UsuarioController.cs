using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalGrupoAlyne.Extension;
using PortalGrupoAlyne.Extension.Helpers;
using PortalGrupoAlyne.Model.Dtos.Usuarios;
using PortalGrupoAlyne.Services;

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
      //  [AllowAnonymous]
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
      //  [AllowAnonymous]
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
        //  [AllowAnonymous]
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


        [HttpGet("userName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllName([FromServices] DataContext context,
 
           [FromQuery] string name
           
           )
        {

            var users = await context.Usuario.AsNoTracking().Where(e => (e.Username.ToLower().Contains(name.ToLower()) 
                                     ))
                         .OrderBy(e => e.Id).ToListAsync();
            users.ForEach(item =>
            {
                item.ImagemURL = GetImagebyUser(item.Username);
            });
            return Ok(users);
        }

       

        [HttpGet("{id}")]
      //  [AllowAnonymous]
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


        [HttpPut("{id}")]
       // [AllowAnonymous]
        public IActionResult Update(int id, UserUpdateResquest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "Usuário atualizado com sucesso" });
        }



        [HttpPost("UploadImage")]
       // [AllowAnonymous]
        public async Task<ActionResult> UploadImage([FromQuery]
        string name)
        
        {
            bool Results = false;
            try
            {
                var _uploadedfiles = Request.Form.Files;
                foreach (IFormFile source in _uploadedfiles)
                {
                    string Filename = name;
                    string Filepath = GetFilePath(Filename);

                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }

                    string imagepath = Filepath + "\\image.png";

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Results = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(Results);
        }
        [NonAction]
       // [AllowAnonymous]
        private string GetFilePath(string UserName)
        {
            return this._environment.WebRootPath + "\\Uploads\\Usuarios\\" + UserName;
        }
        [NonAction]
      //  [AllowAnonymous]
        private string GetImagebyUser(string userName)
        {
            string ImageUrl = string.Empty;
            //string HostUrl = "https://10.0.0.158:8095/";
            string HostUrl = "https://localhost:8095/";
            string Filepath = GetFilePath(userName);
            string Imagepath = Filepath + "\\image.png";
            if (!System.IO.File.Exists(Imagepath))
            {
                ImageUrl = HostUrl + "/uploads/common/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "/uploads/Usuarios/" + userName + "/image.png";
            }
            return ImageUrl;

        }
        [HttpDelete("{id}")]
      //  [AllowAnonymous]
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
