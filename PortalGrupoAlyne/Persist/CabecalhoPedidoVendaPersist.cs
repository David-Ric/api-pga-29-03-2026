using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Persist
{
   
        public interface ICabecalhoPedidoVendaPersist
        {
            Task<CabecalhoPedidoVenda> GetCabecalhoByIdAsync(int Id);
            Task<CabecalhoPedidoVenda> GetCabecalhoByIdWithItemsAsync(int id);
            Task<CabecalhoPedidoVenda> GetCabecalhoByPalMPVWithItemsAsync(string palMPV);
        }
        public class CabecalhoPedidoVendaPersist : ICabecalhoPedidoVendaPersist
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public CabecalhoPedidoVendaPersist(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<CabecalhoPedidoVenda> GetCabecalhoByIdAsync(int Id)
            {
                IQueryable<CabecalhoPedidoVenda> query = _context.CabecalhoPedidoVenda
                    .Include("Vendedor").Include("TipoNegociacao");

                query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == Id);

                return await query.FirstOrDefaultAsync();
            }

            public async Task<CabecalhoPedidoVenda> GetCabecalhoByIdWithItemsAsync(int id)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var queryCabecalho = @"
                        SELECT c.*, v.*, t.* 
                        FROM CabecalhoPedidoVenda c
                        LEFT JOIN Vendedor v ON c.VendedorId = v.Id
                        LEFT JOIN TipoNegociacao t ON c.TipoNegociacaoId = t.Id
                        WHERE c.Id = @Id";

                    var cabecalho = (await connection.QueryAsync<CabecalhoPedidoVenda, Vendedor, TipoNegociacao, CabecalhoPedidoVenda>(
                        queryCabecalho,
                        (c, v, t) =>
                        {
                            c.Vendedor = v;
                            c.TipoNegociacao = t;
                            return c;
                        },
                        new { Id = id },
                        splitOn: "Id,Id" // Assuming Vendedor.Id and TipoNegociacao.Id are the split points
                    )).FirstOrDefault();

                    if (cabecalho != null && !string.IsNullOrEmpty(cabecalho.PalMPV))
                    {
                        var queryItens = @"
                            SELECT i.*, p.*
                            FROM ItemPedidoVenda i
                            LEFT JOIN Produto p ON i.ProdutoId = p.Id
                            WHERE i.PalMPV = @PalMPV";

                        var itens = await connection.QueryAsync<ItemPedidoVenda, Produto, ItemPedidoVenda>(
                            queryItens,
                            (i, p) =>
                            {
                                i.Produto = p;
                                return i;
                            },
                            new { PalMPV = cabecalho.PalMPV },
                            splitOn: "Id" // Assuming Produto.Id is the split point
                        );

                        cabecalho.Itens = itens.ToList();
                    }

                    return cabecalho;
                }
            }

            public async Task<CabecalhoPedidoVenda> GetCabecalhoByPalMPVWithItemsAsync(string palMPV)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var queryCabecalho = @"
                        SELECT c.*, v.*, t.* 
                        FROM CabecalhoPedidoVenda c
                        LEFT JOIN Vendedor v ON c.VendedorId = v.Id
                        LEFT JOIN TipoNegociacao t ON c.TipoNegociacaoId = t.Id
                        WHERE c.PalMPV = @PalMPV";

                    var cabecalho = (await connection.QueryAsync<CabecalhoPedidoVenda, Vendedor, TipoNegociacao, CabecalhoPedidoVenda>(
                        queryCabecalho,
                        (c, v, t) =>
                        {
                            c.Vendedor = v;
                            c.TipoNegociacao = t;
                            return c;
                        },
                        new { PalMPV = palMPV },
                        splitOn: "Id,Id"
                    )).FirstOrDefault();

                    if (cabecalho != null)
                    {
                        var queryItens = @"
                            SELECT i.*, p.*
                            FROM ItemPedidoVenda i
                            LEFT JOIN Produto p ON i.ProdutoId = p.Id
                            WHERE i.PalMPV = @PalMPV";

                        var itens = await connection.QueryAsync<ItemPedidoVenda, Produto, ItemPedidoVenda>(
                            queryItens,
                            (i, p) =>
                            {
                                i.Produto = p;
                                return i;
                            },
                            new { PalMPV = palMPV },
                            splitOn: "Id"
                        );

                        cabecalho.Itens = itens.ToList();
                    }

                    return cabecalho;
                }
            }
        
    }
}