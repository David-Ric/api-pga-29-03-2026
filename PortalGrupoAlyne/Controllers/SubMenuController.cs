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
    public class SubMenuController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private ISubMenuService _subMenuService;

        public SubMenuController(DataContext context, IMapper mapper, ISubMenuService subMenuService)
        {
            _subMenuService = subMenuService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.SubMenu.CountAsync();
            var data = await context.SubMenu.AsNoTracking().Include("Pagina").OrderBy(e => e.Id).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var total = await context.SubMenu.CountAsync();
            var grupos = await context.SubMenu.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
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
            var total = await context.SubMenu.CountAsync();
            var grupos = await context.SubMenu.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => e.Codigo == Codigo).OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = grupos
            });
        }
        [HttpGet("Get-Codigo-MenuMaster")]
        public async Task<IActionResult> GetAllCodigoMenuMaster([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] int Codigo

          )
        {
            var total = await context.SubMenu.CountAsync();
            var grupos = await context.SubMenu.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => e.MenuId == Codigo).OrderBy(e => e.Id).ToListAsync();
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
                var menu = await _subMenuService.GetMenuIdAsync(id);
                if (menu == null) return NoContent();

                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest("Menu não encontrado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<SubMenu>>> AddMenu(SubMenu menu)
        {

            if (_context.SubMenu.Any(u => u.Id == menu.Id))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            _context.SubMenu.Add(menu);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Menu criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, SubMenuDto model)
        {
            _subMenuService.Update(id, model);
            return Ok(new { message = "Menu atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SubMenu>>> Delete(int id)
        {
            var menu = await _context.SubMenu.FindAsync(id);
            if (menu == null)
                return BadRequest("Mrnu não encontrado");

            _context.SubMenu.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok("Menu excluído com sucesso!");
        }
    }
}
