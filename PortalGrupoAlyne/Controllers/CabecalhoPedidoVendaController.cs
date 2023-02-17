using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CabecalhoPedidoVendaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly ICabecalhoPedidoVendaService _cabecalhoPedidoVendaService;
        public CabecalhoPedidoVendaController(DataContext context, IMapper mapper, ICabecalhoPedidoVendaService cabecalhoPedidoVendaService)
        {
            _cabecalhoPedidoVendaService = cabecalhoPedidoVendaService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.CabecalhoPedidoVenda.CountAsync();
            var data = await context.CabecalhoPedidoVenda.Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter/vendedor")]
        public async Task<IActionResult> GetAllFilterCleinteEmpresa([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int codVendedor
   
           )
        {
            var total = await context.CabecalhoPedidoVenda.CountAsync();
            var data = await context.CabecalhoPedidoVenda.Where(e => e.VendedorId == codVendedor).OrderBy(e => e.Id).Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cabecalho = await _cabecalhoPedidoVendaService.GetPedidoVendaByIdAsync(id);
                if (cabecalho == null) return NoContent();

                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                return BadRequest("Pedido não encontrada.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> AddPedido(CabecalhoPedidoVenda tabela)
        {

            if (_context.CabecalhoPedidoVenda.Any(u => u.Id == tabela.Id))
            {
                return BadRequest("Pedido de Venda ja existe na base de dados.");
            }
            
            _context.CabecalhoPedidoVenda.Add(tabela);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Pedido de Venda criado com sucesso." }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, CabecalhoPedidoVendaDto model)
        {
            _cabecalhoPedidoVendaService.Update(id, model);
            return Ok(new { message = "Pedido atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> Delete(int id)
        {
            var pedido = await _context.CabecalhoPedidoVenda.FindAsync(id);
            if (pedido == null)
                return BadRequest("Pedido não encontrada");

            _context.CabecalhoPedidoVenda.Remove(pedido);
            await _context.SaveChangesAsync();

            return Ok("Pedido de Venda excluído com sucesso!");
        }
    }
}
