using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class PaginaPermissaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IPaginaPermissaoService _paginaPermissaoService;

        public PaginaPermissaoController(IMapper mapper, IPaginaPermissaoService paginaPermissaoService, DataContext context)
        {
            _paginaPermissaoService = paginaPermissaoService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.PaginaPermissao.CountAsync();
            var data = await context.PaginaPermissao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var paginas = await context.PaginaPermissao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => e.Codigo == codigo && e.MenuPermissaoId == idMenu)
                         .OrderBy(e => e.Id).ToListAsync();
            return Ok(paginas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaginaPermissao>> Get(int id)
        {
            var pagina = await _context.PaginaPermissao.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada.");
            return Ok(pagina);
        }

        [HttpDelete("menuId")]
        public async Task<ActionResult> DeleteAllPagesId(int menuId)
        {
            try
            {
                var paginas = await _paginaPermissaoService.GetPaginasPorMenuId(menuId);
                if (paginas == null) return NoContent();
                _context.PaginaPermissao.RemoveRange(paginas);
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
        public async Task<ActionResult<List<PaginaPermissao>>> AddProduto(PaginaPermissao pagina)
        {

            if (_context.PaginaPermissao.Any(u => u.Id == pagina.Id))
            {
                return BadRequest("Página ja existe na base de dados.");
            }
            if (_context.PaginaPermissao.Any(u => u.MenuPermissaoId == pagina.MenuPermissaoId && u.Codigo == pagina.Codigo))
            {
                return BadRequest("Menu ja existe na base de dados.");
            }
            _context.PaginaPermissao.Add(pagina);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Página criada com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, PaginaPermissaoDto model)
        {
            _paginaPermissaoService.Update(id, model);
            return Ok(new { message = "Página atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PaginaPermissao>>> Delete(int id)
        {
            var pagina = await _context.PaginaPermissao.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada");

            _context.PaginaPermissao.Remove(pagina);
            await _context.SaveChangesAsync();

            return Ok("Página excluída com sucesso!");
        }

    }
}
