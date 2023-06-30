using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Extension;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaPrecoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private ITabelaPrecoService _tabelaPrecoService;

        public TabelaPrecoController(DataContext context, IMapper mapper, ITabelaPrecoService tabelaPrecoService)
        {
            _tabelaPrecoService = tabelaPrecoService;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.TabelaPreco.CountAsync();
            var data = await context.TabelaPreco.AsNoTracking()
                .Include(i => i.ItemTabela)
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
            
            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("total")]

        public async Task<IActionResult> GetAllTodos([FromServices] DataContext context)
        {
            var total = await context.TabelaPreco.CountAsync();
            var data = await context.TabelaPreco.AsNoTracking()
                .Include(i => i.ItemTabela)
                .ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        //============filter nome ============================================================
        [HttpGet("filter/nome")]
        public async Task<IActionResult> GetAllFilterNome([FromServices] DataContext context,
                [FromQuery] int pagina,
                 [FromQuery] int totalpagina,
                 [FromQuery] string filter
                )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.TabelaPreco
            .Where(e => (e.Descricao.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Include(i => i.ItemTabela)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.TabelaPreco
            .AsNoTracking()
                 .Where(e => (e.Descricao.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Include(i => i.ItemTabela)
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


        }
        //===========foilter codigo ========================================================
        [HttpGet("filter/codigo")]
        public async Task<IActionResult> GetAllFilterCodigo([FromServices] DataContext context,
               [FromQuery] int pagina,
                [FromQuery] int totalpagina,
                [FromQuery] int filter
               )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.TabelaPreco
            .Where(e => (e.Id==filter))
                .OrderBy(e => e.Id)
                .Include(i => i.ItemTabela)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.TabelaPreco
            .AsNoTracking()
                .Where(e => (e.Id == filter))
                .OrderBy(e => e.Id)
                .Include(i => i.ItemTabela)
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


        }

        //===================================================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tabela = await _tabelaPrecoService.GetTabelaByIdAsync(id);
                if (tabela == null) return NoContent();
             
                return Ok(tabela);
            }
            catch (Exception ex)
            {
               return BadRequest("Tabela de preço não encontrada.");
            }
        }

       



        [HttpPost]
        public async Task<ActionResult<List<TabelaPreco>>> AddTabelaPreco(TabelaPreco tabela)
        {

            if (_context.TabelaPreco.Any(u => u.Id == tabela.Id))
            {
                return BadRequest("Tabela de preço ja existe na base de dados.");
            }
            _context.TabelaPreco.Add(tabela);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Tabela de preço criada com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, TabelaPrecoDto model)
        {
            _tabelaPrecoService.Update(id, model);
            return Ok(new { message = "Tabela de preço atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TabelaPreco>>> Delete(int id)
        {
            var tabela = await _context.TabelaPreco.FindAsync(id);
            if (tabela == null)
                return BadRequest("Tabela de preço não encontrada");

            _context.TabelaPreco.Remove(tabela);
            await _context.SaveChangesAsync();

            return Ok("Tabela de preço excluída com sucesso!");
        }

    }
}
