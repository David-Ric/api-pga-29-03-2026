using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Data;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using System.Net;
using System.Text.Json;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabecalhoOrcamentoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        public CabecalhoOrcamentoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public class NovoOrcamentoRequest
        {
            public CabecalhoOrcamentoDto CabecalhoOrcamento { get; set; } = null!;
            public List<ItemOrcamentoDto> ItensOrcamento { get; set; } = new();
        }

        private static string SomenteDigitos(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            return new string(value.Where(char.IsDigit).ToArray());
        }

        private sealed class BrasilApiCnpjResponse
        {
            public string? cnpj { get; set; }
            public string? razao_social { get; set; }
            public string? nome_fantasia { get; set; }
            public string? logradouro { get; set; }
            public string? numero { get; set; }
            public string? complemento { get; set; }
            public string? bairro { get; set; }
            public string? municipio { get; set; }
            public string? uf { get; set; }
            public string? cep { get; set; }
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> ConsultarCnpj(string cnpj, CancellationToken cancellationToken)
        {
            var cnpjLimpo = SomenteDigitos(cnpj);
            if (cnpjLimpo.Length != 14)
            {
                return BadRequest("CNPJ inválido.");
            }

            var url = $"https://brasilapi.com.br/api/cnpj/v1/{cnpjLimpo}";
            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            using var resp = await _http.SendAsync(req, cancellationToken);

            if (resp.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound("CNPJ não encontrado.");
            }

            if (!resp.IsSuccessStatusCode)
            {
                return StatusCode((int)resp.StatusCode, "Falha ao consultar CNPJ.");
            }

            var json = await resp.Content.ReadAsStringAsync(cancellationToken);
            var data = JsonSerializer.Deserialize<BrasilApiCnpjResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null)
            {
                return BadRequest("Resposta inválida ao consultar CNPJ.");
            }

            return Ok(new
            {
                cnpj = data.cnpj,
                nome = data.razao_social ?? data.nome_fantasia,
                razaoSocial = data.razao_social,
                nomeFantasia = data.nome_fantasia,
                endereco = data.logradouro,
                numero = data.numero,
                complemento = data.complemento,
                bairro = data.bairro,
                cidade = data.municipio,
                uf = data.uf,
                cep = data.cep
            });
        }

        [HttpPost("envio")]
        public async Task<ActionResult> Envio([FromBody] NovoOrcamentoRequest request)
        {
            if (request == null || request.CabecalhoOrcamento == null)
            {
                return BadRequest("Cabeçalho é obrigatório.");
            }

            var cabecalhoDto = request.CabecalhoOrcamento;
            var itensDto = request.ItensOrcamento ?? new List<ItemOrcamentoDto>();

            if (string.IsNullOrWhiteSpace(cabecalhoDto.PedidoId))
            {
                return BadRequest("PedidoId é obrigatório.");
            }

            if (cabecalhoDto.Valor <= 0)
            {
                return BadRequest("O Valor do Orçamento não pode ser igual a zero.");
            }

            var itensNormalizados = itensDto
                .Where(i => i != null)
                .GroupBy(i => i.ProdutoId)
                .Select(g => g.Last())
                .ToList();

            var itensEntities = itensNormalizados.Select(item => new ItemOrcamento
            {
                Filial = item.Filial,
                VendedorId = item.VendedorId,
                PedidoId = cabecalhoDto.PedidoId,
                ProdutoId = item.ProdutoId,
                Quant = item.Quant,
                ValUnit = item.ValUnit,
                ValTotal = item.ValTotal,
                Baixado = item.Baixado,
                Inativo = string.IsNullOrWhiteSpace(item.Inativo) ? "N" : item.Inativo
            }).ToList();

            var itensAtivosCount = itensEntities.Count(i => !string.Equals(i.Inativo, "S", StringComparison.OrdinalIgnoreCase));

            var cabecalho = new CabecalhoOrcamento
            {
                Id = cabecalhoDto.Id,
                Filial = cabecalhoDto.Filial,
                Lote = cabecalhoDto.Lote,
                VendedorId = cabecalhoDto.VendedorId,
                PedidoId = cabecalhoDto.PedidoId,
                TabelaPrecoId = cabecalhoDto.TabelaPrecoId,
                TipoNegociacaoId = cabecalhoDto.TipoNegociacaoId,
                CnpjCpf = cabecalhoDto.CnpjCpf,
                NomeParceiro = cabecalhoDto.NomeParceiro,
                EndParceiro = cabecalhoDto.EndParceiro,
                NumeroEnd = cabecalhoDto.NumeroEnd,
                ComplementoEnd = cabecalhoDto.ComplementoEnd,
                Bairro = cabecalhoDto.Bairro,
                Cidade = cabecalhoDto.Cidade,
                UF = cabecalhoDto.UF,
                CEP = cabecalhoDto.CEP,
                Data = cabecalhoDto.Data,
                Valor = cabecalhoDto.Valor,
                DataEntrega = cabecalhoDto.DataEntrega,
                Observacao = cabecalhoDto.Observacao,
                Baixado = cabecalhoDto.Baixado,
                Orcamento = cabecalhoDto.Orcamento,
                Status = cabecalhoDto.Status,
                TipPed = cabecalhoDto.TipPed,
                Ativo = cabecalhoDto.Ativo,
                Versao = cabecalhoDto.Versao,
                Quant_Itens = itensAtivosCount,
                Log_Envio = cabecalhoDto.Log_Envio
            };

            CabecalhoOrcamento cabecalhoFinal = null;

            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var produtoIds = itensEntities.Select(i => i.ProdutoId).Distinct().ToList();
                    var itensExistentes = produtoIds.Count == 0
                        ? new List<ItemOrcamento>()
                        : await _context.ItemOrcamento
                            .Where(i => i.PedidoId == cabecalho.PedidoId && produtoIds.Contains(i.ProdutoId))
                            .ToListAsync();

                    var itensExistentesPorProduto = itensExistentes.ToDictionary(i => i.ProdutoId, i => i);

                    foreach (var item in itensEntities)
                    {
                        if (itensExistentesPorProduto.TryGetValue(item.ProdutoId, out var itemDb))
                        {
                            itemDb.Filial = item.Filial;
                            itemDb.VendedorId = item.VendedorId;
                            itemDb.PedidoId = item.PedidoId;
                            itemDb.Quant = item.Quant;
                            itemDb.ValUnit = item.ValUnit;
                            itemDb.ValTotal = item.ValTotal;
                            itemDb.Baixado = item.Baixado;
                            itemDb.Inativo = item.Inativo;
                        }
                        else
                        {
                            item.Id = 0;
                            item.Produto = null;
                            item.Vendedor = null;
                            _context.ItemOrcamento.Add(item);
                        }
                    }

                    if (produtoIds.Count > 0)
                    {
                        var itensParaInativar = await _context.ItemOrcamento
                            .Where(i => i.PedidoId == cabecalho.PedidoId && !produtoIds.Contains(i.ProdutoId) && (i.Inativo == null || i.Inativo != "S"))
                            .ToListAsync();

                        foreach (var it in itensParaInativar)
                        {
                            it.Inativo = "S";
                        }
                    }

                    await _context.SaveChangesAsync();

                    var cabecalhoExistente = await _context.CabecalhoOrcamento.FirstOrDefaultAsync(p => p.PedidoId == cabecalho.PedidoId);
                    if (cabecalhoExistente != null)
                    {
                        cabecalhoExistente.Data = cabecalho.Data;
                        cabecalhoExistente.DataEntrega = cabecalho.DataEntrega;
                        cabecalhoExistente.Filial = cabecalho.Filial;
                        cabecalhoExistente.Observacao = cabecalho.Observacao;
                        cabecalhoExistente.Status = cabecalho.Status;
                        cabecalhoExistente.TipPed = cabecalho.TipPed;
                        cabecalhoExistente.TipoNegociacaoId = cabecalho.TipoNegociacaoId;
                        cabecalhoExistente.TabelaPrecoId = cabecalho.TabelaPrecoId;
                        cabecalhoExistente.Valor = cabecalho.Valor;
                        cabecalhoExistente.VendedorId = cabecalho.VendedorId;
                        cabecalhoExistente.Ativo = cabecalho.Ativo;
                        cabecalhoExistente.Versao = cabecalho.Versao;
                        cabecalhoExistente.Quant_Itens = cabecalho.Quant_Itens;
                        cabecalhoExistente.Log_Envio = cabecalho.Log_Envio;
                        cabecalhoExistente.PedidoId = cabecalho.PedidoId;
                        cabecalhoExistente.CnpjCpf = cabecalho.CnpjCpf;
                        cabecalhoExistente.NomeParceiro = cabecalho.NomeParceiro;
                        cabecalhoExistente.EndParceiro = cabecalho.EndParceiro;
                        cabecalhoExistente.NumeroEnd = cabecalho.NumeroEnd;
                        cabecalhoExistente.ComplementoEnd = cabecalho.ComplementoEnd;
                        cabecalhoExistente.Bairro = cabecalho.Bairro;
                        cabecalhoExistente.Cidade = cabecalho.Cidade;
                        cabecalhoExistente.UF = cabecalho.UF;
                        cabecalhoExistente.CEP = cabecalho.CEP;
                        cabecalhoExistente.Orcamento = cabecalho.Orcamento;
                        cabecalhoFinal = cabecalhoExistente;
                    }
                    else
                    {
                        cabecalho.Vendedor = null;
                        cabecalho.TipoNegociacao = null;
                        cabecalho.Itens = null;
                        if (string.IsNullOrWhiteSpace(cabecalho.Ativo))
                        {
                            cabecalho.Ativo = "S";
                        }
                        if (string.IsNullOrWhiteSpace(cabecalho.Status))
                        {
                            cabecalho.Status = "Aberto";
                        }

                        _context.CabecalhoOrcamento.Add(cabecalho);
                        cabecalhoFinal = cabecalho;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });

            if (cabecalhoFinal == null)
            {
                return BadRequest("Erro ao Salvar Orçamento.");
            }

            return Ok(new
            {
                message = "Operação concluída com sucesso.",
                id = cabecalhoFinal.Id,
                pedidoId = cabecalhoFinal.PedidoId,
                status = cabecalhoFinal.Status,
                orcamento = cabecalhoFinal.Orcamento,
                logEnvio = cabecalhoFinal.Log_Envio
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
            [FromQuery] int totalpagina)
        {
            var total = await context.CabecalhoOrcamento.CountAsync();
            var data = await context.CabecalhoOrcamento
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

        [HttpGet("filter/vendedor")]
        public async Task<IActionResult> GetAllFilterVendedor([FromServices] DataContext context,
            [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int codVendedor)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var query = context.CabecalhoOrcamento
                .AsNoTracking()
                .Where(e => e.VendedorId == codVendedor);

            var total = await query.CountAsync();
            var data = await query
                .OrderByDescending(e => e.Data)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return Ok(new
            {
                total,
                data
            });
        }

        [HttpGet("pedidoId/{pedidoId}")]
        public async Task<IActionResult> GetByPedidoId(string pedidoId)
        {
            var pedido = pedidoId?.Trim();
            if (string.IsNullOrWhiteSpace(pedido))
            {
                return BadRequest("PedidoId é obrigatório.");
            }

            var cabecalho = await _context.CabecalhoOrcamento
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.PedidoId == pedido);

            if (cabecalho == null)
            {
                return NoContent();
            }

            var itens = await _context.ItemOrcamento
                .AsNoTracking()
                .Where(i => i.PedidoId == pedido)
                .Include("Produto")
                .ToListAsync();

            return Ok(new
            {
                cabecalho,
                itens
            });
        }

        [HttpPut("pedidoId/{pedidoId}/cancelar")]
        public async Task<IActionResult> CancelarByPedidoId(string pedidoId)
        {
            var pedido = pedidoId?.Trim();
            if (string.IsNullOrWhiteSpace(pedido))
            {
                return BadRequest("PedidoId é obrigatório.");
            }

            var cabecalho = await _context.CabecalhoOrcamento.FirstOrDefaultAsync(c => c.PedidoId == pedido);
            if (cabecalho == null)
            {
                return NotFound("Orçamento não encontrado.");
            }

            cabecalho.Status = "Cancelado";
            cabecalho.Ativo = "N";

            var itens = await _context.ItemOrcamento
                .Where(i => i.PedidoId == pedido)
                .ToListAsync();

            foreach (var it in itens)
            {
                it.Inativo = "S";
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Orçamento cancelado com sucesso.",
                id = cabecalho.Id,
                pedidoId = cabecalho.PedidoId,
                status = cabecalho.Status,
                ativo = cabecalho.Ativo
            });
        }
    }
}
