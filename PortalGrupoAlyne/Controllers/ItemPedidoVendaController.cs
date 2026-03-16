using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PortalGrupoAlyne.Model;
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
        private readonly IConfiguration _configuration;
        public ItemPedidoVendaController(DataContext context, IMapper mapper, IItemPedidoVendaService itemPedidoVendaService, IConfiguration configuration)
        {
            _itemPedidoVendaService = itemPedidoVendaService;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
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


        //[HttpPost]
        //public async Task<ActionResult<List<ItemPedidoVenda>>> AddItemPedido(List<ItemPedidoVendaDto> itens)
        //{
        //    try
        //    {
        //        if (itens == null || itens.Count == 0)
        //        {
        //            return BadRequest("Não é permitido salvar uma lista vazia.");
        //        }

        //        List<string> mensagensErro = new List<string>();
        //        List<int> itensNaoSalvos = new List<int>();

        //        foreach (var item in itens)
        //        {
        //            if (item.Quant <= 0)
        //            {
        //                mensagensErro.Add($"Item com ProdutoId {item.ProdutoId}: A Quantidade não pode ser menor ou igual a zero.");
        //            }
        //            else if (item.ValUnit <= 0)
        //            {
        //                mensagensErro.Add($"O produto: {item.ProdutoId} está com o valor unitário zerado");
        //            }
        //            else if (item.ValTotal <= 0)
        //            {
        //                mensagensErro.Add($"Item com ProdutoId {item.ProdutoId}: O Valor Total não pode ser igual a zero.");
        //            }
        //            else
        //            {
        //                var existingItems = _context.ItemPedidoVenda.Where(u => u.PalMPV == item.PalMPV).ToList();
        //                foreach (var existingItem in existingItems)
        //                {
        //                    _context.ItemPedidoVenda.Remove(existingItem);
        //                }

        //                var itemPedidoVenda = new ItemPedidoVenda
        //                {
        //                    Filial = item.Filial,
        //                    VendedorId = item.VendedorId,
        //                    PalMPV = item.PalMPV,
        //                    ProdutoId = item.ProdutoId,
        //                    Quant = item.Quant,
        //                    ValUnit = item.ValUnit,
        //                    ValTotal = item.ValTotal,
        //                    Baixado = item.Baixado
        //                };

        //                _context.ItemPedidoVenda.Add(itemPedidoVenda);
        //            }
        //        }

        //        if (mensagensErro.Count > 0)
        //        {
        //            await _context.SaveChangesAsync();
        //            return Ok(new { message = "Alguns itens não puderam ser salvos.", errors = mensagensErro });
        //        }
        //        else
        //        {
        //            await _context.SaveChangesAsync();
        //            return Ok(new { message = "Pedido de Venda criado com sucesso." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Erro ao criar pedido de venda.");
        //    }
        //}
        /// SEGUNDA FORMA
        //[HttpPost]
        //public async Task<ActionResult<List<ItemPedidoVenda>>> AddItemPedido(List<ItemPedidoVendaDto> itens)
        //{
        //    try
        //    {
        //        if (itens == null || itens.Count == 0)
        //        {
        //            return BadRequest("Não é permitido salvar uma lista vazia.");
        //        }

        //        List<string> mensagensErro = new List<string>();
        //        List<int> itensNaoSalvos = new List<int>();

        //        // Filtrar os itens para remover duplicatas com o mesmo ProdutoId
        //        var itensFiltrados = itens.GroupBy(i => new { i.PalMPV, i.ProdutoId })
        //                         .Select(group => group.First())
        //                         .ToList();

        //        foreach (var item in itensFiltrados)
        //        {
        //            if (item.Quant <= 0)
        //            {
        //                mensagensErro.Add($"Item com ProdutoId {item.ProdutoId}: A Quantidade não pode ser menor ou igual a zero.");
        //            }
        //            else if (item.ValUnit <= 0)
        //            {
        //                mensagensErro.Add($"O produto: {item.ProdutoId} está com o valor unitário zerado");
        //            }
        //            else if (item.ValTotal <= 0)
        //            {
        //                mensagensErro.Add($"Item com ProdutoId {item.ProdutoId}: O Valor Total não pode ser igual a zero.");
        //            }
        //            else
        //            {
        //                var existingItems = _context.ItemPedidoVenda.Where(u => u.PalMPV == item.PalMPV).ToList();
        //                foreach (var existingItem in existingItems)
        //                {
        //                    _context.ItemPedidoVenda.Remove(existingItem);
        //                }

        //                var itemPedidoVenda = new ItemPedidoVenda
        //                {
        //                    Filial = item.Filial,
        //                    VendedorId = item.VendedorId,
        //                    PalMPV = item.PalMPV,
        //                    ProdutoId = item.ProdutoId,
        //                    Quant = item.Quant,
        //                    ValUnit = item.ValUnit,
        //                    ValTotal = item.ValTotal,
        //                    Baixado = item.Baixado
        //                };

        //                _context.ItemPedidoVenda.Add(itemPedidoVenda);
        //            }
        //        }

        //        if (mensagensErro.Count > 0)
        //        {
        //            await _context.SaveChangesAsync();
        //            return Ok(new { message = "Alguns itens não puderam ser salvos.", errors = mensagensErro });
        //        }
        //        else
        //        {
        //            await _context.SaveChangesAsync();
        //            return Ok(new { message = "Pedido de Venda criado com sucesso." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Erro ao criar pedido de venda.");
        //    }
        //}


        [HttpPost("item")]
        public async Task<ActionResult> AddItemPedido(ItemPedidoVendaDto item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("O objeto de item é nulo.");
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

                var existingItem = _context.ItemPedidoVenda.FirstOrDefault(u => u.PalMPV == item.PalMPV && u.ProdutoId == item.ProdutoId);

                if (existingItem != null)
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

                await _context.SaveChangesAsync();

                return Ok(new { message = "Pedido de Venda criado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar pedido de venda.");
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

                var itensFiltrados = itens.GroupBy(i => new { i.PalMPV, i.ProdutoId })
                                     .Select(group => group.First())
                                     .ToList();

                foreach (var item in itensFiltrados)
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
                    }
                }

              
                await _context.SaveChangesAsync();

                foreach (var item in itensFiltrados)
                {
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

                
                await _context.SaveChangesAsync();

                if (mensagensErro.Count > 0)
                {
                    return Ok(new { message = "Alguns itens não puderam ser salvos.", errors = mensagensErro });
                }
                else
                {
                    return Ok(new { message = "Pedido de Venda criado com sucesso." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar pedido de venda.");
            }
        }




        [HttpPost("Lista")]
        public async Task<ActionResult> UpdateOrInsertItems(List<ItemPedidoVendaDto> novosItens)
        {
            try
            {
                if (novosItens == null || novosItens.Count == 0)
                {
                    return BadRequest("Não é permitido salvar uma lista vazia.");
                }

                var grupos = novosItens
                    .Where(i => !string.IsNullOrWhiteSpace(i.PalMPV))
                    .GroupBy(i => i.PalMPV!.Trim())
                    .ToList();

                if (grupos.Count == 0)
                {
                    return BadRequest("Não é permitido salvar itens sem PalMPV.");
                }

                var mySqlCon = _configuration.GetConnectionString("DefaultConnection");
                using var connection = new MySqlConnection(mySqlCon);
                await connection.OpenAsync();
                using var transaction = await connection.BeginTransactionAsync();

                var totalEsperado = 0;
                foreach (var grupo in grupos)
                {
                    var pal = grupo.Key;

                    var itensDedup = grupo
                        .Where(i => i.ProdutoId != null)
                        .GroupBy(i => i.ProdutoId)
                        .Select(g => g.Last())
                        .ToList();

                    if (itensDedup.Count == 0)
                    {
                        continue;
                    }

                    totalEsperado += itensDedup.Count;
                    var deleteParams = new DynamicParameters();
                    deleteParams.Add("PalMPV", pal);

                    var produtoIdParams = new List<string>(itensDedup.Count);
                    for (var i = 0; i < itensDedup.Count; i++)
                    {
                        var produtoParam = $"ProdutoId_{i}";
                        produtoIdParams.Add("@" + produtoParam);
                        deleteParams.Add(produtoParam, itensDedup[i].ProdutoId);
                    }

                    var deleteSql =
                        $"DELETE FROM ItemPedidoVenda WHERE PalMPV = @PalMPV AND ProdutoId NOT IN ({string.Join(", ", produtoIdParams)})";
                    await connection.ExecuteAsync(deleteSql, deleteParams, transaction);

                    var upsertParams = new DynamicParameters();
                    var valuesSql = new List<string>(itensDedup.Count);

                    for (var i = 0; i < itensDedup.Count; i++)
                    {
                        var item = itensDedup[i];
                        var produtoId = item.ProdutoId;
                        if (produtoId == null) continue;

                        var filialParam = $"Filial_{i}";
                        var vendedorParam = $"VendedorId_{i}";
                        var produtoParam = $"ProdutoId_{i}";
                        var quantParam = $"Quant_{i}";
                        var valUnitParam = $"ValUnit_{i}";
                        var valTotalParam = $"ValTotal_{i}";
                        var baixadoParam = $"Baixado_{i}";

                        valuesSql.Add(
                            $"(@{filialParam}, @{vendedorParam}, @PalMPV, @{produtoParam}, @{quantParam}, @{valUnitParam}, @{valTotalParam}, @{baixadoParam})");

                        upsertParams.Add(filialParam, item.Filial);
                        upsertParams.Add(vendedorParam, item.VendedorId);
                        upsertParams.Add(produtoParam, produtoId);
                        upsertParams.Add(quantParam, item.Quant);
                        upsertParams.Add(valUnitParam, item.ValUnit);
                        upsertParams.Add(valTotalParam, item.ValTotal);
                        upsertParams.Add(baixadoParam, item.Baixado);
                    }

                    if (valuesSql.Count > 0)
                    {
                        upsertParams.Add("PalMPV", pal);

                        var upsertSql =
                            "INSERT INTO ItemPedidoVenda (Filial, VendedorId, PalMPV, ProdutoId, Quant, ValUnit, ValTotal, Baixado) VALUES " +
                            string.Join(", ", valuesSql) +
                            " ON DUPLICATE KEY UPDATE Filial = VALUES(Filial), VendedorId = VALUES(VendedorId), Quant = VALUES(Quant), ValUnit = VALUES(ValUnit), ValTotal = VALUES(ValTotal), Baixado = VALUES(Baixado)";

                        await connection.ExecuteAsync(upsertSql, upsertParams, transaction);
                    }
                }

                await transaction.CommitAsync();

                var palList = grupos.Select(g => g.Key).ToList();
                var selectParams = new DynamicParameters();
                var palPlaceholders = new List<string>(palList.Count);
                for (var i = 0; i < palList.Count; i++)
                {
                    var p = $"Pal_{i}";
                    palPlaceholders.Add("@" + p);
                    selectParams.Add(p, palList[i]);
                }

                var selectSql =
                    $"SELECT COUNT(*) FROM ItemPedidoVenda WHERE PalMPV IN ({string.Join(", ", palPlaceholders)})";

                var totalSalvos = await connection.ExecuteScalarAsync<int>(selectSql, selectParams);

                return Ok(new { totalSalvos });
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

        [HttpDelete("palmpv/{palmpv}/produto/{produtoId}")]
        public async Task<ActionResult<List<ItemPedidoVenda>>> DeleteByPalMPVAndProdutoId(string palmpv, int produtoId)
        {
            var item = await _context.ItemPedidoVenda
                .Where(i => i.PalMPV == palmpv && i.ProdutoId == produtoId)
                .FirstOrDefaultAsync();

            if (item == null)
                return BadRequest("Item do pedido de venda não encontrado");

            _context.ItemPedidoVenda.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Item do pedido de venda excluído com sucesso!");
        }



    }
}
