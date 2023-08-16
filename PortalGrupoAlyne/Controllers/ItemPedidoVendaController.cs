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

        [HttpGet("filter/vendedorId")]
        public async Task<IActionResult> GetAllFilterItemAll([FromServices] DataContext context,[FromQuery] int codVendedor)
        {
            
            var data = await context.ItemPedidoVenda
                .AsNoTracking()
                .Where(e => e.VendedorId == codVendedor)
                .OrderBy(e => e.Id).Include(i => i.Produto)
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
                if (itens == null || itens.Count == 0)
                {
                    return BadRequest("Não é permitido salvar uma lista vazia.");
                }

                List<string> mensagensErro = new List<string>();
                List<int> itensNaoSalvos = new List<int>();

                foreach (var item in itens)
                {
                    if (item.Quant <= 0)
                    {
                        mensagensErro.Add($"Item com ProdutoId {item.ProdutoId}: A Quantidade não pode ser menor ou igual a zero.");
                    }
                    else if (item.ValUnit <= 0)
                    {
                        mensagensErro.Add($"O produto: {item.ProdutoId} está com o valor unitário zerado");
                    }
                    else if (item.ValTotal <= 0)
                    {
                        mensagensErro.Add($"Item com ProdutoId {item.ProdutoId}: O Valor Total não pode ser igual a zero.");
                    }
                    else
                    {
                        var existingItems = _context.ItemPedidoVenda.Where(u => u.PalMPV == item.PalMPV).ToList();
                        foreach (var existingItem in existingItems)
                        {
                            _context.ItemPedidoVenda.Remove(existingItem);
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

                        _context.ItemPedidoVenda.Add(itemPedidoVenda);
                    }
                }

                if (mensagensErro.Count > 0)
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Alguns itens não puderam ser salvos.", errors = mensagensErro });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Pedido de Venda criado com sucesso." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar pedido de venda.");
            }
        }





        [HttpPost("Lista")]
        public async Task<ActionResult<List<ItemPedidoVenda>>> UpdateOrInsertItems(List<ItemPedidoVendaDto> novosItens)
        {
            try
            {
                if (novosItens == null || novosItens.Count == 0)
                {
                    return BadRequest("Não é permitido salvar uma lista vazia.");
                }

                List<string> mensagensErro = new List<string>();

                foreach (var novoItem in novosItens)
                {
                    if (novoItem.PalMPV == null)
                    {
                        mensagensErro.Add("Item sem PalMPV definido.");
                        continue;
                    }

                    var itensMesmoPalMPV = _context.ItemPedidoVenda.Where(item => item.PalMPV == novoItem.PalMPV).ToList();

                    foreach (var itemExistente in itensMesmoPalMPV)
                    {
                        var itemCorrespondente = novoItem.ProdutoId == itemExistente.ProdutoId ? novoItem : null;

                        if (itemCorrespondente != null)
                        {
                            // Atualizar propriedades do item existente com base no item correspondente
                            itemExistente.Quant = itemCorrespondente.Quant;
                            itemExistente.ValUnit = itemCorrespondente.ValUnit;
                            itemExistente.ValTotal = itemCorrespondente.ValTotal;
                            itemExistente.Baixado = itemCorrespondente.Baixado;
                        }
                        else
                        {
                            // Caso o item correspondente não seja encontrado, ele será removido
                            _context.ItemPedidoVenda.Remove(itemExistente);
                        }
                    }

                    if (novoItem.ProdutoId != null)
                    {
                        var itemExistente = itensMesmoPalMPV.FirstOrDefault(item => item.ProdutoId == novoItem.ProdutoId);

                        if (itemExistente == null)
                        {
                            // Inserir novo item na tabela
                            _context.ItemPedidoVenda.Add(new ItemPedidoVenda
                            {
                                Filial = novoItem.Filial,
                                VendedorId = novoItem.VendedorId,
                                PalMPV = novoItem.PalMPV,
                                ProdutoId = novoItem.ProdutoId,
                                Quant = novoItem.Quant,
                                ValUnit = novoItem.ValUnit,
                                ValTotal = novoItem.ValTotal,
                                Baixado = novoItem.Baixado
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();

                if (mensagensErro.Count > 0)
                {
                    return Ok(new { message = "Alguns itens não puderam ser atualizados ou inseridos.", errors = mensagensErro });
                }
                else
                {
                    return Ok(new { message = "Itens atualizados e/ou inseridos com sucesso." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar ou inserir itens.");
            }
        }







        [HttpPut("{id}")]

        public IActionResult Update(int id, ItemPedidoVendaDto model)
        {
            _itemPedidoVendaService.Update(id, model);

            if (_context.ItemPedidoVenda.Any(u => u.ValUnit <= 0))
            {
                return BadRequest("O Valor Unitário não pode ser igual a zero.");
            }
            if (_context.ItemPedidoVenda.Any(u => u.ValTotal <= 0))
            {
                return BadRequest("O Valor Total não pode ser igual a zero.");
            }

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
