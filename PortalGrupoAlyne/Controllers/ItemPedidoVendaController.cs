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
            var total = await context.ItemPedidoVenda.CountAsync();
            var data = await context.ItemPedidoVenda.Where(e => e.VendedorId == codVendedor).OrderBy(e => e.Id).Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter/codProduto")]
        public async Task<IActionResult> GetAllFilterCodProduto([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int codProduto

           )
        {
            var total = await context.ItemPedidoVenda.CountAsync();
            var data = await context.ItemPedidoVenda.Where(e => e.ProdutoId == codProduto && e.ProdutoId == codProduto).OrderBy(e => e.Id).Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter/produto")]
        public async Task<IActionResult> GetAllFilterProduto([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] string produto

           )
        {
            var total = await context.ItemPedidoVenda.CountAsync();
            var data = await context.ItemPedidoVenda.Where(e => (e.Produto.Nome.ToLower().Contains(produto.ToLower()))).OrderBy(e => e.Id).Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter/grupoProduto")]
        public async Task<IActionResult> GetAllFilterProduto([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int grupoProduto

           )
        {
            var total = await context.ItemPedidoVenda.CountAsync();
            var data = await context.ItemPedidoVenda.Where(e => e.Produto.GrupoProdutoId == grupoProduto).OrderBy(e => e.Id).Include("Vendedor").Include("tipoNegociacao").Include("ItemPedidoVenda").Include("ItemPedidoVenda.Produto").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
