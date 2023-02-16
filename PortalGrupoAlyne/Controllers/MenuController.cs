using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IMenuService _menuService;

        public MenuController(DataContext context, IMapper mapper, IMenuService menuService)
        {
            _menuService = menuService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.Menu.CountAsync();
            var data = await context.Menu.AsNoTracking().Include("SubMenu").Include("SubMenu.Pagina").Include("Pagina").OrderBy(e => e.Id).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var total = await context.Menu.CountAsync();
            var grupos = await context.Menu.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => (e.Nome.ToLower().Contains(Nome.ToLower()))).OrderBy(e => e.Id).ToListAsync();
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
            var total = await context.Menu.CountAsync();
            var grupos = await context.Menu.AsNoTracking().Include("SubMenu").Include("SubMenu.Pagina").Include("Pagina").Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => e.Codigo == Codigo).OrderBy(e => e.Id).ToListAsync();
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
                var menu = await _menuService.GetMenuIdAsync(id);
                if (menu == null) return NoContent();

                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest("Menu não encontrado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Menu>>> AddMenu(Menu menu)
        {

            if (_context.Menu.Any(u => u.Id == menu.Id))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            if (_context.Menu.Any(u => u.Codigo == menu.Codigo))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Menu criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, MenuDto model)
        {
            _menuService.Update(id, model);
            return Ok(new { message = "Menu atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Menu>>> Delete(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
                return BadRequest("Mrnu não encontrado");

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok("Menu excluído com sucesso!");
        }
    }
}
