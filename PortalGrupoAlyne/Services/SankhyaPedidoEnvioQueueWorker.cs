using Dapper;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using PortalGrupoAlyne.Model;
using SankhyaDtos = PortalGrupoAlyne.Model.Dtos.Sankhya;
using System;
using System.Linq;

namespace PortalGrupoAlyne.Services
{
    public class SankhyaPedidoEnvioQueueWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SankhyaPedidoEnvioQueueWorker> _logger;

        public SankhyaPedidoEnvioQueueWorker(IConfiguration configuration, ILogger<SankhyaPedidoEnvioQueueWorker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessarProximoPedidoAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar fila de envio de pedidos ao Sankhya.");
                }

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

        private async Task ProcessarProximoPedidoAsync(CancellationToken stoppingToken)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            await using var con = new MySqlConnection(connectionString);
            await con.OpenAsync(stoppingToken);

            var lockOk = await con.ExecuteScalarAsync<int>("SELECT GET_LOCK('SANKHYA_ENVIO_PEDIDO_VENDA', 0);");
            if (lockOk != 1)
            {
                return;
            }

            try
            {
                var cabecalho = await con.QueryFirstOrDefaultAsync<CabecalhoPedidoVenda>(
                    @"SELECT *
                      FROM CabecalhoPedidoVenda
                      WHERE Status IN ('EmEnvio', 'AProcessar')
                      ORDER BY CASE WHEN Status = 'EmEnvio' THEN 0 ELSE 1 END, Id
                      LIMIT 1;");

                if (cabecalho == null)
                {
                    return;
                }

                if (string.Equals(cabecalho.Status, "AProcessar", StringComparison.OrdinalIgnoreCase))
                {
                    var rows = await con.ExecuteAsync(
                        @"UPDATE CabecalhoPedidoVenda
                          SET Status = 'EmEnvio'
                          WHERE Id = @Id AND Status = 'AProcessar';",
                        new { cabecalho.Id });

                    if (rows == 0)
                    {
                        return;
                    }

                    cabecalho.Status = "EmEnvio";
                }

                var itens = (await con.QueryAsync<ItemPedidoVenda>(
                    @"SELECT *
                      FROM ItemPedidoVenda
                      WHERE PalMPV = @PalMPV AND (Inativo IS NULL OR Inativo <> 'S');",
                    new { PalMPV = cabecalho.PalMPV })).ToList();

                if (itens.Count == 0)
                {
                    await con.ExecuteAsync(
                        @"UPDATE CabecalhoPedidoVenda
                          SET Status = 'Falhou',
                              Log_Envio = @Log
                          WHERE Id = @Id;",
                        new { cabecalho.Id, Log = "Nenhum item ativo encontrado para envio." });
                    return;
                }

                if (cabecalho.Quant_Itens.HasValue && cabecalho.Quant_Itens.Value != itens.Count)
                {
                    await con.ExecuteAsync(
                        @"UPDATE CabecalhoPedidoVenda
                          SET Status = 'Falhou',
                              Log_Envio = @Log
                          WHERE Id = @Id;",
                        new
                        {
                            cabecalho.Id,
                            Log = $"Quantidade de itens divergente. Cabeçalho: {cabecalho.Quant_Itens.Value}. Itens ativos no banco: {itens.Count}."
                        });
                    return;
                }

                string? resultadoEnvio = null;
                string? numeroPedidoSankhya = null;
                try
                {
                    resultadoEnvio = await SankhyaService.ExecuteWithLoginLogout(_configuration, async () =>
                    {
                        var pedidoReq = new SankhyaDtos.PedidoVendaRequest
                        {
                            CabecalhoPedidoVenda = new SankhyaDtos.CabecalhoPedidoVenda
                            {
                                Id = cabecalho.Id,
                                Filial = cabecalho.Filial,
                                VendedorId = cabecalho.VendedorId,
                                PalmPV = cabecalho.PalMPV,
                                TipoNegociacaoId = cabecalho.TipoNegociacaoId,
                                TipPed = cabecalho.TipPed,
                                ParceiroId = cabecalho.ParceiroId,
                                Data = cabecalho.Data,
                                Valor = Convert.ToDecimal(cabecalho.Valor ?? 0),
                                DataEntrega = cabecalho.DataEntrega,
                                Observacao = cabecalho.Observacao
                            },
                            ItemPedidoVenda = itens.Select(i => new SankhyaDtos.ItemPedidoVenda
                            {
                                Id = i.Id,
                                Filial = i.Filial,
                                VendedorId = i.VendedorId,
                                PalMPV = i.PalMPV,
                                ProdutoId = i.ProdutoId,
                                Quant = Convert.ToDecimal(i.Quant ?? 0),
                                ValUnit = Convert.ToDecimal(i.ValUnit ?? 0),
                                ValTotal = Convert.ToDecimal(i.ValTotal ?? 0),
                                Baixado = i.Baixado
                            }).ToList()
                        };

                        var envio = await SankhyaService.EnviarPedidoItensPrimeiro(_configuration, pedidoReq);
                        var resultado = envio?.ToString();

                        if (string.Equals(resultado, "Sucesso", StringComparison.OrdinalIgnoreCase))
                        {
                            var palSql = (cabecalho.PalMPV ?? "").Replace("'", "''");
                            var query = $"SELECT TOP 1 PEDIDO FROM AD_Z38 (NOLOCK) WHERE PALMPV = '{palSql}' AND PEDIDO IS NOT NULL AND LTRIM(RTRIM(PEDIDO)) <> ''";
                            for (var tentativaPedido = 0; tentativaPedido < 12; tentativaPedido++)
                            {
                                SankhyaDtos.QueryResponse respQuery = await SankhyaService.executeQuery(_configuration, query);
                                if (respQuery != null && respQuery.status == "1" && respQuery.responseBody.rows != null && respQuery.responseBody.rows.Count > 0)
                                {
                                    var row = respQuery.responseBody.rows[0];
                                    if (row != null && row.Count > 0)
                                    {
                                        var value = row[0];
                                        var vStr = value?.ToString();
                                        if (!string.IsNullOrWhiteSpace(vStr) && vStr != "0")
                                        {
                                            numeroPedidoSankhya = vStr;
                                            break;
                                        }
                                    }
                                }

                                await Task.Delay(TimeSpan.FromMilliseconds(800 + (tentativaPedido * 250)), stoppingToken);
                            }
                        }
                        return resultado;
                    });
                }
                catch (Exception ex)
                {
                    resultadoEnvio = ex.Message;
                }

                if (string.Equals(resultadoEnvio, "Sucesso", StringComparison.OrdinalIgnoreCase))
                {
                    await con.ExecuteAsync(
                        @"UPDATE CabecalhoPedidoVenda
                          SET Status = 'Enviado',
                              Log_Envio = NULL,
                              pedido = COALESCE(NULLIF(@Pedido, ''), pedido)
                          WHERE Id = @Id;",
                        new { cabecalho.Id, Pedido = numeroPedidoSankhya ?? "" });
                }
                else
                {
                    await con.ExecuteAsync(
                        @"UPDATE CabecalhoPedidoVenda
                          SET Status = 'Falhou',
                              Log_Envio = @Log
                          WHERE Id = @Id;",
                        new { cabecalho.Id, Log = resultadoEnvio ?? "Falhou" });
                }
            }
            finally
            {
                await con.ExecuteAsync("DO RELEASE_LOCK('SANKHYA_ENVIO_PEDIDO_VENDA');");
            }
        }
    }
}
