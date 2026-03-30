using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Data;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemOrcamentoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ItemOrcamentoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
            [FromQuery] int totalpagina)
        {
            var total = await context.ItemOrcamento.CountAsync();
            var data = await context.ItemOrcamento
                .Include("Produto")
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina)
                .ToListAsync();

            return Ok(new
            {
                total,
                data
            });
        }

        [HttpGet("filter/pedidoId")]
        public async Task<IActionResult> GetAllFilterPedidoId([FromServices] DataContext context,
            [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] string pedidoId)
        {
            var pedido = pedidoId?.Trim();
            if (string.IsNullOrWhiteSpace(pedido))
            {
                return BadRequest("PedidoId é obrigatório.");
            }

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemOrcamento
                .AsNoTracking()
                .Where(e => e.PedidoId == pedido)
                .OrderBy(e => e.Id)
                .Include("Produto")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemOrcamento
                .AsNoTracking()
                .Where(e => e.PedidoId == pedido)
                .CountAsync();

            return Ok(new
            {
                total,
                data
            });
        }

        [HttpPost("item")]
        public async Task<ActionResult> AddItemOrcamento(ItemOrcamentoDto item)
        {
            if (item == null)
            {
                return BadRequest("O objeto de item é nulo.");
            }

            if (string.IsNullOrWhiteSpace(item.PedidoId))
            {
                return BadRequest("PedidoId é obrigatório.");
            }

            if (item.Quant <= 0)
            {
                return BadRequest($"Item com ProdutoId {item.ProdutoId}: A Quantidade não pode ser menor ou igual a zero.");
            }
            else if (item.ValUnit <= 0)
            {
                return BadRequest($"O produto: {item.ProdutoId} está com o valor unitário zerado.");
            }
            else if (item.ValTotal <= 0)
            {
                return BadRequest($"Item com ProdutoId {item.ProdutoId}: O Valor Total não pode ser igual a zero.");
            }

            var existingItem = await _context.ItemOrcamento
                .FirstOrDefaultAsync(u => u.PedidoId == item.PedidoId && u.ProdutoId == item.ProdutoId);

            if (existingItem != null)
            {
                existingItem.Filial = item.Filial;
                existingItem.VendedorId = item.VendedorId;
                existingItem.PedidoId = item.PedidoId;
                existingItem.Quant = item.Quant;
                existingItem.ValUnit = item.ValUnit;
                existingItem.ValTotal = item.ValTotal;
                existingItem.Baixado = item.Baixado;
                existingItem.Inativo = string.IsNullOrWhiteSpace(item.Inativo) ? "N" : item.Inativo;
            }
            else
            {
                var novoItem = new ItemOrcamento
                {
                    Id = 0,
                    Filial = item.Filial,
                    VendedorId = item.VendedorId,
                    PedidoId = item.PedidoId,
                    ProdutoId = item.ProdutoId,
                    Quant = item.Quant,
                    ValUnit = item.ValUnit,
                    ValTotal = item.ValTotal,
                    Baixado = item.Baixado,
                    Inativo = string.IsNullOrWhiteSpace(item.Inativo) ? "N" : item.Inativo,
                    Produto = null,
                    Vendedor = null
                };
                _context.ItemOrcamento.Add(novoItem);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Item salvo com sucesso." });
        }
    }
}
