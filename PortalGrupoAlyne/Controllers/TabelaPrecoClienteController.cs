using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaPrecoClienteController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly ITabela_Preco_ClienteService _tabela_Preco_ClienteService;
        public TabelaPrecoClienteController(DataContext context, IMapper mapper, ITabela_Preco_ClienteService tabela_Preco_ClienteService)
        {
            _tabela_Preco_ClienteService = tabela_Preco_ClienteService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.TabelaPrecoCliente.CountAsync();
            var data = await context.TabelaPrecoCliente.Include(e => e.TabelaPreco).Include(p => p.Parceiros).AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<Tabela_Preco_Cliente>> Get(int id)
        //{
        //    var tabela = await _context.Tabela_Preco_Cliente.FindAsync(id);
        //    if (tabela == null)
        //        return BadRequest("Tabela não encontrada.");
        //    return Ok(tabela);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tabela = await _tabela_Preco_ClienteService.GetTabelaClienteAsync(id);
                if (tabela == null) return NoContent();

                return Ok(tabela);
            }
            catch (Exception ex)
            {
                return BadRequest("Tabela de preço não encontrada.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<TabelaPrecoCliente>>> AddTabela(TabelaPrecoCliente tabela)
        {

            if (_context.TabelaPrecoCliente.Any(u => u.id== tabela.id))
            {
                return BadRequest("Esta tabela ja existe na base de dados.");
            }
            _context.TabelaPrecoCliente.Add(tabela);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Tabela de preço cliente criada com sucesso." }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, TabelaPrecoClienteDto model)
        {
            _tabela_Preco_ClienteService.Update(id, model);
            return Ok(new { message = "Tabela de preço do cliente atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TabelaPrecoCliente>>> Delete(int id)
        {
            var tabela = await _context.TabelaPrecoCliente.FindAsync(id);
            if (tabela == null)
                return BadRequest("Tabela de preço cliente não encontrada");

            _context.TabelaPrecoCliente.Remove(tabela);
            await _context.SaveChangesAsync();

            return Ok("Tabela de preço cliente excluída com sucesso!");
        }
    }
}
