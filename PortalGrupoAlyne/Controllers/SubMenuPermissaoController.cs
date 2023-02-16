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
    public class SubMenuPermissaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private ISubMenuPermissaoService _subMenuPermissaoService;

        public SubMenuPermissaoController(DataContext context, IMapper mapper, ISubMenuPermissaoService subMenuPermissaoService)
        {
            _subMenuPermissaoService = subMenuPermissaoService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.SubMenuPermissao.CountAsync();
            var data = await context.SubMenuPermissao.AsNoTracking().Include("PaginaPermissao").OrderBy(e => e.Id).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var total = await context.SubMenuPermissao.CountAsync();
            var grupos = await context.SubMenuPermissao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
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
            var total = await context.SubMenuPermissao.CountAsync();
            var grupos = await context.SubMenuPermissao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
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
            var total = await context.SubMenuPermissao.CountAsync();
            var grupos = await context.SubMenuPermissao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => e.MenuPermissaoId == Codigo).OrderBy(e => e.Id).ToListAsync();
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
                var menu = await _subMenuPermissaoService.GetMenuIdAsync(id);
                if (menu == null) return NoContent();

                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest("Menu não encontrado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<SubMenuPermissao>>> AddMenu(SubMenuPermissao menu)
        {

            if (_context.SubMenuPermissao.Any(u => u.Id == menu.Id))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            if (_context.SubMenuPermissao.Any(u => u.MenuPermissaoId == menu.MenuPermissaoId && u.Codigo == menu.Codigo))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            _context.SubMenuPermissao.Add(menu);
            await _context.SaveChangesAsync();

            return Ok((new { menu, message = "Menu criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, SubMenuPermissaoDto model)
        {
            _subMenuPermissaoService.Update(id, model);
            return Ok(new { message = "Menu atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SubMenuPermissao>>> Delete(int id)
        {
            var menu = await _context.SubMenuPermissao.FindAsync(id);
            if (menu == null)
                return BadRequest("Mrnu não encontrado");

            _context.SubMenuPermissao.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok("Menu excluído com sucesso!");
        }
    }
}
