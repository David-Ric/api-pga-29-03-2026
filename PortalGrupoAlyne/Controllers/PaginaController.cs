using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IPaginaService _paginaService;

        public PaginaController(IMapper mapper, IPaginaService paginaService, DataContext context)
        {
            _paginaService = paginaService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Pagina.CountAsync();
            var data = await context.Pagina.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).OrderBy(e => e.Id).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("codigo")]

        public async Task<IActionResult> GetAllFilter([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
         [FromQuery] int codigo,
         [FromQuery] int idMenu

         )
        {
          //  var total = await context.Pagina.CountAsync();
            var paginas = await context.Pagina.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => e.Codigo==codigo && e.MenuId==idMenu)
                         .OrderBy(e => e.Id).ToListAsync();
            return Ok(paginas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pagina>> Get(int id)
        {
           var pagina = await _context.Pagina.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada.");
            return Ok(pagina);
        }

        [HttpDelete("menuCod")]
        public async Task<ActionResult> DeleteAllPagesId(int menuCod)
        {
            try
            {
                var paginas = await _paginaService.GetPaginasPorMenuId(menuCod);
                if (paginas == null) return NoContent();
                _context.Pagina.RemoveRange(paginas);
                await _context.SaveChangesAsync();

                return Ok("Paginas excluídas");
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar paginas. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Pagina>>> AddProduto(Pagina pagina)
        {

            if (_context.Pagina.Any(u => u.Id == pagina.Id))
            {
                return BadRequest("Página ja existe na base de dados.");
            }
            if (_context.Pagina.Any(u => u.MenuId == pagina.MenuId && u.Codigo==pagina.Codigo))
            {
                return BadRequest("Página ja existe na base de dados.");
            }
            _context.Pagina.Add(pagina);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Página criada com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, PaginaDto model)
        {
            _paginaService.Update(id, model);
            return Ok(new { message = "Página atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Pagina>>> Delete(int id)
        {
            var pagina = await _context.Pagina.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada");

            _context.Pagina.Remove(pagina);
            await _context.SaveChangesAsync();

            return Ok("Página excluída com sucesso!");
        }

    }
}
