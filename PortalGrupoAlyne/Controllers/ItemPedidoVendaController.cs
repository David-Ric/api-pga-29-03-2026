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
    public class ItemPedidoVendaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly IItemPedidoVendaService _itemPedidoVendaService;
        public ItemPedidoVendaController(DataContext context, IMapper mapper, IItemPedidoVendaService itemPedidoVendaService)
        {
            _itemPedidoVendaService = itemPedidoVendaService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.ItemPedidoVenda.CountAsync();
            var data = await context.ItemPedidoVenda.Include("Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.VendedorId == codVendedor)
                .OrderBy(e => e.Id).Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.VendedorId == codVendedor)
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

           
        }

        

        [HttpGet("filter/pedidoId")]
        public async Task<IActionResult> GetAllFilterPedidoId([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
           [FromQuery] int pedidoId

          )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.CabecalhoPedidoVendaId == pedidoId)
                .OrderBy(e => e.Id).Include("Vendedor").Include("Produto")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.CabecalhoPedidoVendaId == pedidoId)
                .CountAsync();

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
                var item = await _itemPedidoVendaService.GetItemPedidoVendaByIdAsync(id);
                if (item == null) return NoContent();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest("Item não encontrada.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<ItemPedidoVenda>>> AddItemPedido(ItemPedidoVenda item)
        {

            if (_context.ItemPedidoVenda.Any(u => u.Id == item.Id))
            {
                return BadRequest("Item do pedido de venda ja existe na base de dados.");
            }
            if (_context.ItemPedidoVenda.Any(u => u.PalMPV == item.PalMPV && u.ProdutoId==item.ProdutoId))
            {
                return BadRequest("Item ja foi adicionado ao pedido.");
            }

            _context.ItemPedidoVenda.Add(item);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Pedido de Venda criado com sucesso." }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, ItemPedidoVendaDto model)
        {
            _itemPedidoVendaService.Update(id, model);
            return Ok(new { message = "Item do pedido de venda atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ItemPedidoVenda>>> Delete(int id)
        {
            var item = await _context.ItemPedidoVenda.FindAsync(id);
            if (item == null)
                return BadRequest("Item do pedido de venda não encontrada");

            _context.ItemPedidoVenda.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Item do pedido de venda excluído com sucesso!");
        }
    }
}
