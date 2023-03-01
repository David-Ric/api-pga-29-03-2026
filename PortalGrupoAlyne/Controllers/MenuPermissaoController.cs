using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPermissaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IMenuPermissoesService _menuPermissoesService;

        public MenuPermissaoController(DataContext context, IMapper mapper, IMenuPermissoesService menuPermissoesService)
        {
            _menuPermissoesService = menuPermissoesService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.MenuPermissao.CountAsync();
            var data = await context.MenuPermissao.AsNoTracking().Include("SubMenuPermissao").Include("SubMenuPermissao.PaginaPermissao").Include("PaginaPermissao").OrderBy(e => e.Id).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter-UserId")]

        public async Task<IActionResult> GetFilterUserId([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int userId
           )
        {
            
            var data = await context.MenuPermissao.AsNoTracking().Include("SubMenuPermissao").Include("SubMenuPermissao.PaginaPermissao").Include("PaginaPermissao").Where(e=>e.UsuarioId==userId).OrderBy(e => e.Id).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();
            var total = data.Count();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("Get-Nome")]
        public async Task<IActionResult> GetAllName([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
         [FromQuery] string Nome

         )
        {
            
            var grupos = await context.MenuPermissao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => (e.Nome.ToLower().Contains(Nome.ToLower()))).OrderBy(e => e.Id).ToListAsync();
            var total = grupos.Count();
            return Ok(new
            {
                total,
                data = grupos
            });
        }

        [HttpGet("Get-Codigo")]
        public async Task<IActionResult> GetAllCodigo([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] int Codigo

          )
        {
            
            var grupos = await context.MenuPermissao.AsNoTracking().Include("SubMenuPermissao").Include("SubMenuPermissao.PaginaPermissao").Include("PaginaPermissao").Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => e.Codigo == Codigo).OrderBy(e => e.Id).ToListAsync();
            var total = grupos.Count();
            return Ok(new
            {
                total,
                data = grupos
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var menu = await _menuPermissoesService.GetMenuIdAsync(id);
                if (menu == null) return NoContent();

                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest("Menu não encontrado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<MenuPermissao>>> AddMenu(MenuPermissao menu)
        {

            if (_context.MenuPermissao.Any(u => u.Id == menu.Id))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            if (_context.MenuPermissao.Any(u => u.UsuarioId ==menu.UsuarioId  && u.Codigo == menu.Codigo))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            _context.MenuPermissao.Add(menu);
            await _context.SaveChangesAsync();

            return Ok((new {menu, message = "Menu criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, MenuPermissaoDto model)
        {
            _menuPermissoesService.Update(id, model);
            return Ok(new { message = "Menu atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MenuPermissao>>> Delete(int id)
        {
            var menu = await _context.MenuPermissao.FindAsync(id);
            if (menu == null)
                return BadRequest("Menu não encontrado");

            _context.MenuPermissao.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok("Menu excluído com sucesso!");
        }
    }
}
