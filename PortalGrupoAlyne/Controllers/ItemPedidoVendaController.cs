using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
   // [Authorize]
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
           [FromQuery] string pedidoId

          )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.PalMPV== pedidoId)
                .OrderBy(e => e.Id).Include("Vendedor").Include("Produto")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.PalMPV == pedidoId)
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
        public async Task<ActionResult<List<ItemPedidoVenda>>> AddItemPedido(List<ItemPedidoVendaDto> itens)
        {
            try
            {
                List<ItemPedidoVenda> itemPedidoVendaList = new List<ItemPedidoVenda>();

                foreach (var item in itens)
                {
                    if (item.Quant <= 0)
                    {
                        return BadRequest("A Quantidade não pode ser menor ou igual a zero.");
                    }

                    var itemPedidoVenda = new ItemPedidoVenda
                    {
                        Filial = item.Filial,
                        VendedorId = item.VendedorId,
                        PalMPV = item.PalMPV,
                        ProdutoId = item.ProdutoId,
                        Quant = item.Quant,
                        ValUnit = item.ValUnit,
                        ValTotal = item.ValTotal,
                        Baixado = item.Baixado
                    };

                    if (_context.ItemPedidoVenda.Any(u => u.Id == itemPedidoVenda.Id))
                    {
                        return BadRequest("Item do pedido de venda já existe na base de dados.");
                    }

                    itemPedidoVendaList.Add(itemPedidoVenda);
                }

                _context.ItemPedidoVenda.AddRange(itemPedidoVendaList);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Pedido de Venda criado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar pedido de venda.");
            }
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

        [HttpDelete("palmpv/{palmpv}")]
        public async Task<ActionResult<List<ItemPedidoVenda>>> DeleteByPalMPV(string palmpv)
        {
            var itens = await _context.ItemPedidoVenda.Where(i => i.PalMPV == palmpv).ToListAsync();
            if (itens.Count == 0)
                return BadRequest("Itens do pedido de venda não encontrados");

            _context.ItemPedidoVenda.RemoveRange(itens);
            await _context.SaveChangesAsync();

            return Ok("Itens do pedido de venda excluídos com sucesso!");
        }


    }
}
