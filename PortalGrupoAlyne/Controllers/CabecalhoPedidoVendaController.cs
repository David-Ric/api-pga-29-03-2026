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
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CabecalhoPedidoVendaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly ICabecalhoPedidoVendaService _cabecalhoPedidoVendaService;
        private readonly IConfiguration _configuration;
        public CabecalhoPedidoVendaController(DataContext context, IMapper mapper, ICabecalhoPedidoVendaService cabecalhoPedidoVendaService, IConfiguration configuration)
        {
            _cabecalhoPedidoVendaService = cabecalhoPedidoVendaService;
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
            var total = await context.CabecalhoPedidoVenda.CountAsync();
            var data = await context.CabecalhoPedidoVenda.Include("Vendedor").Include("TipoNegociacao").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }


        [HttpGet("ultimos/vendedor")]
        public async Task<IActionResult> Get05ultumos([FromServices] DataContext context, [FromQuery] int codVendedor)
        {
            var cabecalho = await context.CabecalhoPedidoVenda
                .Where(e => e.Vendedor.Id == codVendedor && e.Ativo !="N")
                .OrderByDescending(e => e.Data)
                .Take(60)
                .Include("Vendedor")
                .Include("TipoNegociacao")
                .AsNoTracking()
                .ToListAsync();

            var palMPVs = cabecalho.Select(c => c.PalMPV); 

            var itens = await context.ItemPedidoVenda
                .Where(item => palMPVs.Contains(item.PalMPV)) 
                .Include("Produto")
                .AsNoTracking()
                .ToListAsync();

            var totalCabecalho = cabecalho.Count();

            return Ok(new
            {
                totalCabecalho,
                cabecalho,
                itens
            });
        }

        [HttpGet("Processar")]
        public async Task<IActionResult> GetAllProcessar([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina

         )
        {

            var data = await context.CabecalhoPedidoVenda
                .Where(e => e.Status == "Processar")
                .OrderByDescending(e => e.Id)
                //.Include("Vendedor")
                //.Include("TipoNegociacao")
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
            var total = data.Count();
            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("Processar/Vendedor")]
        public async Task<IActionResult> GetProcessarVend([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
          [FromQuery] int codVendedor

         )
        {

            var data = await context.CabecalhoPedidoVenda
                .Where(e => e.VendedorId == codVendedor && e.Status == "Processar")
                .OrderByDescending(e => e.Id)
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
            var total = data.Count();
            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("Processar/Cliente")]
        public async Task<IActionResult> GetProcessarCliente([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
          [FromQuery] int codCliente

         )
        {

            var data = await context.CabecalhoPedidoVenda
                .Where(e => e.ParceiroId == codCliente && e.Status == "Processar")
                .OrderByDescending(e => e.Id)
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
            var total = data.Count();
            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("Processar/PalMPV")]
        public async Task<IActionResult> GetProcessarPalMPV([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
          [FromQuery] string palm

         )
        {

            var data = await context.CabecalhoPedidoVenda
                 .Where(e => e.PalMPV.ToLower().Contains(palm.ToLower()) && e.Status == "Processar")
                .OrderByDescending(e => e.Id)
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
            var total = data.Count();
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

            var data = await context.CabecalhoPedidoVenda
                .Where(e => e.Vendedor.Id == codVendedor && e.Ativo !="N")
                .OrderBy(e => e.Id)
                .Include("Vendedor")
                .Include("TipoNegociacao")
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
            var total = data.Count();
            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter/status")]
        public async Task<IActionResult> GetAllFilterPedisoStatus([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
          [FromQuery] int codVendedor,
          [FromQuery] int codParceiro,
          [FromQuery] string status
         )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            List<CabecalhoPedidoVenda> data;
            var total = 0;

            if (status == "todos")
            {
                data =
                await context.CabecalhoPedidoVenda
                .Where(e => e.Vendedor.Id == codVendedor && e.ParceiroId == codParceiro && e.Ativo != "N")
                .OrderByDescending(e => e.Id)
                .Include("Vendedor")
                .Include("TipoNegociacao")
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();

                total = await context.CabecalhoPedidoVenda
                .AsNoTracking()
                .Where(e => e.Vendedor.Id == codVendedor && e.ParceiroId == codParceiro && e.Ativo != "N")
                .CountAsync();

            }
            else
            {
                data =
                await context.CabecalhoPedidoVenda
                .Where(e => e.Vendedor.Id == codVendedor && e.ParceiroId == codParceiro && e.Status.Trim() == status && e.Ativo != "N")
                .OrderByDescending(e => e.Id)
                .Include("Vendedor")
                .Include("TipoNegociacao")
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();
                total = await context.CabecalhoPedidoVenda
                .AsNoTracking()
                .Where(e => e.Vendedor.Id == codVendedor && e.ParceiroId == codParceiro && e.Status.Trim() == status && e.Ativo != "N")
                .CountAsync();

            }
 
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
                var cabecalho = await _cabecalhoPedidoVendaService.GetPedidoVendaByIdWithItemsAsync(id);
                if (cabecalho == null) return NoContent();

                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                return BadRequest("Pedido não encontrada.");
            }
        }

        [HttpGet("palmpv/{palMPV}")]
        public async Task<IActionResult> GetByPalMPV(string palMPV)
        {
            try
            {
                var cabecalho = await _cabecalhoPedidoVendaService.GetPedidoVendaByPalMPVWithItemsAsync(palMPV);
                if (cabecalho == null) return NoContent();

                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                return BadRequest("Pedido não encontrada.");
            }
        }
        //[HttpPost]
        //public async Task<ActionResult<List<CabecalhoPedidoVenda>>> AddPedido(CabecalhoPedidoVenda tabela)
        //{
        //    if (tabela.Valor <= 0)
        //    {
        //        return BadRequest("O Valor do Pedido não pode ser igual a zero.");
        //    }

        //    var existingPedido = await _context.CabecalhoPedidoVenda.FirstOrDefaultAsync(u => u.PalMPV == tabela.PalMPV);

        //    if (existingPedido != null)
        //    {

        //        existingPedido.Data = tabela.Data;
        //        existingPedido.DataEntrega = tabela.DataEntrega;
        //        existingPedido.Filial = tabela.Filial;
        //        existingPedido.Observacao = tabela.Observacao;
        //        existingPedido.PalMPV = tabela.PalMPV;
        //        existingPedido.ParceiroId = tabela.ParceiroId;
        //        existingPedido.pedido = tabela.pedido;
        //        existingPedido.Status = tabela.Status;
        //        existingPedido.TipPed = tabela.TipPed;
        //        existingPedido.TipoNegociacaoId = tabela.TipoNegociacaoId;
        //        existingPedido.Valor = tabela.Valor;
        //        existingPedido.VendedorId = tabela.VendedorId;
        //        existingPedido.Ativo = tabela.Ativo;
        //        existingPedido.Versao = tabela.Versao;


        //        _context.CabecalhoPedidoVenda.Update(existingPedido);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { data = existingPedido, message = "Pedido de Venda existente atualizado com sucesso." });
        //    }

        //    _context.CabecalhoPedidoVenda.Add(tabela);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { data = tabela, message = "Pedido de Venda criado com sucesso." });
        //}

        [HttpGet("listagem")]
        public async Task<ActionResult<IEnumerable<CabecalhoPedidoVenda>>> GetCabecalhoPedidoVenda()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                // Sua consulta personalizada
                string selectQuery = "SELECT * FROM CabecalhoPedidoVenda";

                var cabecalhoPedidoVenda = await connection.QueryAsync<CabecalhoPedidoVenda>(selectQuery);

                if (!cabecalhoPedidoVenda.Any())
                {
                    return NotFound(); // Retorna 404 se não houver registros.
                }

                return cabecalhoPedidoVenda.ToList();
            }
        }





        [HttpPost]
        public async Task<ActionResult> AddOrUpdatePedido(CabecalhoPedidoVenda tabela)
        {
            if (tabela.Valor <= 0)
            {
                return BadRequest("O Valor do Pedido não pode ser igual a zero.");
            }

            string MySqlCon = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(MySqlCon))
            {

                try
                {

                    connection.Open();
                    var palMPV = tabela.PalMPV;
                    var selectQuery = "SELECT * FROM CabecalhoPedidoVenda WHERE PalMPV = @PalMPV";
                    MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
                    cmd.Parameters.AddWithValue("@PalMPV", palMPV);

                    //

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            connection.Close();
                            connection.Open();
                            var updateQuery = "UPDATE CabecalhoPedidoVenda SET Data = @Data, DataEntrega = @DataEntrega, Filial = @Filial, Observacao = @Observacao, PalMPV = @PalMPV, ParceiroId = @ParceiroId, pedido = @pedido, Status = @Status, TipPed = @TipPed, TipoNegociacaoId = @TipoNegociacaoId, Valor = @Valor, VendedorId = @VendedorId, Ativo = @Ativo, Versao = @Versao WHERE PalMPV = @PalMPV";
                            cmd = new MySqlCommand(updateQuery, connection);
                            cmd.Parameters.AddWithValue("@Data", tabela.Data);
                            cmd.Parameters.AddWithValue("@DataEntrega", tabela.DataEntrega);
                            cmd.Parameters.AddWithValue("@Filial", tabela.Filial);
                            cmd.Parameters.AddWithValue("@Observacao", tabela.Observacao);
                            cmd.Parameters.AddWithValue("@PalMPV", tabela.PalMPV);
                            cmd.Parameters.AddWithValue("@ParceiroId", tabela.ParceiroId);
                            cmd.Parameters.AddWithValue("@pedido", tabela.pedido);
                            cmd.Parameters.AddWithValue("@Status", tabela.Status);
                            cmd.Parameters.AddWithValue("@TipPed", tabela.TipPed);
                            cmd.Parameters.AddWithValue("@TipoNegociacaoId", tabela.TipoNegociacaoId);
                            cmd.Parameters.AddWithValue("@Valor", tabela.Valor);
                            cmd.Parameters.AddWithValue("@VendedorId", tabela.VendedorId);
                            cmd.Parameters.AddWithValue("@Ativo", tabela.Ativo);
                            cmd.Parameters.AddWithValue("@Versao", tabela.Versao);

                            cmd.ExecuteNonQuery();

                            connection.Close();
                        }
                        else
                        {
                            connection.Close();
                            connection.Open();
                            var insertQuery = "INSERT INTO CabecalhoPedidoVenda (Data, DataEntrega, Filial, Observacao, PalMPV, ParceiroId, pedido, Status, TipPed, TipoNegociacaoId, Valor, VendedorId, Ativo, Versao) VALUES (@Data, @DataEntrega, @Filial, @Observacao, @PalMPV, @ParceiroId, @pedido, @Status, @TipPed, @TipoNegociacaoId, @Valor, @VendedorId, @Ativo, @Versao)";
                            cmd = new MySqlCommand(insertQuery, connection);
                            cmd.Parameters.AddWithValue("@Data", tabela.Data);
                            cmd.Parameters.AddWithValue("@DataEntrega", tabela.DataEntrega);
                            cmd.Parameters.AddWithValue("@Filial", tabela.Filial);
                            cmd.Parameters.AddWithValue("@Observacao", tabela.Observacao);
                            cmd.Parameters.AddWithValue("@PalMPV", tabela.PalMPV);
                            cmd.Parameters.AddWithValue("@ParceiroId", tabela.ParceiroId);
                            cmd.Parameters.AddWithValue("@pedido", tabela.pedido);
                            cmd.Parameters.AddWithValue("@Status", tabela.Status);
                            cmd.Parameters.AddWithValue("@TipPed", tabela.TipPed);
                            cmd.Parameters.AddWithValue("@TipoNegociacaoId", tabela.TipoNegociacaoId);
                            cmd.Parameters.AddWithValue("@Valor", tabela.Valor);
                            cmd.Parameters.AddWithValue("@VendedorId", tabela.VendedorId);
                            cmd.Parameters.AddWithValue("@Ativo", tabela.Ativo);
                            cmd.Parameters.AddWithValue("@Versao", tabela.Versao);

                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }

                    return Ok("Operação concluída com sucesso.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Ocorreu um erro durante a conexão. {ex.Message}");
                }
            }
        }



        //[HttpPost("Lista")]
        //public ActionResult AddOrDeletePedidosLista(List<CabecalhoPedidoVenda> listaPedidos)
        //{
        //    try
        //    {
        //        if (listaPedidos == null || listaPedidos.Count == 0)
        //        {
        //            return BadRequest("A lista de pedidos está vazia ou nula.");
        //        }

        //        string MySqlCon = _configuration.GetConnectionString("DefaultConnection");

        //        using (var connection = new MySqlConnection(MySqlCon))
        //        {
        //            connection.Open();
        //            using (var transaction = connection.BeginTransaction())
        //            {
        //                try
        //                {
        //                    foreach (var tabela in listaPedidos)
        //                    {
        //                        if (tabela.Valor <= 0)
        //                        {
        //                            transaction.Rollback();
        //                            return BadRequest("O Valor do Pedido não pode ser igual a zero.");
        //                        }

        //                        var palMPV = tabela.PalMPV;
        //                        var deleteQuery = "DELETE FROM CabecalhoPedidoVenda WHERE PalMPV = @PalMPV";
        //                        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection, transaction);
        //                        deleteCmd.Parameters.AddWithValue("@PalMPV", palMPV);
        //                        deleteCmd.ExecuteNonQuery();

        //                        var insertQuery = "INSERT INTO CabecalhoPedidoVenda (Data, DataEntrega, Filial, Observacao, PalMPV, ParceiroId, pedido, Status, TipPed, TipoNegociacaoId, Valor, VendedorId, Ativo, Versao) VALUES (@Data, @DataEntrega, @Filial, @Observacao, @PalMPV, @ParceiroId, @pedido, @Status, @TipPed, @TipoNegociacaoId, @Valor, @VendedorId, @Ativo, @Versao)";
        //                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection, transaction);
        //                        insertCmd.Parameters.AddWithValue("@Data", tabela.Data);
        //                        insertCmd.Parameters.AddWithValue("@DataEntrega", tabela.DataEntrega);
        //                        insertCmd.Parameters.AddWithValue("@Filial", tabela.Filial);
        //                        insertCmd.Parameters.AddWithValue("@Observacao", tabela.Observacao);
        //                        insertCmd.Parameters.AddWithValue("@PalMPV", tabela.PalMPV);
        //                        insertCmd.Parameters.AddWithValue("@ParceiroId", tabela.ParceiroId);
        //                        insertCmd.Parameters.AddWithValue("@pedido", tabela.pedido);
        //                        insertCmd.Parameters.AddWithValue("@Status", tabela.Status);
        //                        insertCmd.Parameters.AddWithValue("@TipPed", tabela.TipPed);
        //                        insertCmd.Parameters.AddWithValue("@TipoNegociacaoId", tabela.TipoNegociacaoId);
        //                        insertCmd.Parameters.AddWithValue("@Valor", tabela.Valor);
        //                        insertCmd.Parameters.AddWithValue("@VendedorId", tabela.VendedorId);
        //                        insertCmd.Parameters.AddWithValue("@Ativo", tabela.Ativo);
        //                        insertCmd.Parameters.AddWithValue("@Versao", tabela.Versao);
        //                        insertCmd.ExecuteNonQuery();
        //                    }

        //                    transaction.Commit();

        //                    return Ok("Operação concluída com sucesso.");
        //                }
        //                catch (Exception ex)
        //                {
        //                    transaction.Rollback();
        //                    return StatusCode(500, $"Ocorreu um erro durante a transação. {ex.Message}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Ocorreu um erro durante a conexão. {ex.Message}");
        //    }
        //}







        //[HttpPost("Lista")]
        //public async Task<ActionResult<List<CabecalhoPedidoVenda>>> AddOrUpdatePedido(List<CabecalhoPedidoVenda> listaTabelas)
        //{

        //    foreach (var tabela in listaTabelas)
        //    {
        //        var existingTabela = await _context.CabecalhoPedidoVenda.FirstOrDefaultAsync(u => u.PalMPV == tabela.PalMPV);

        //        if (existingTabela != null)
        //        {
        //            existingTabela.Data = tabela.Data;
        //            existingTabela.DataEntrega = tabela.DataEntrega;
        //            existingTabela.Filial = tabela.Filial;
        //            existingTabela.Observacao = tabela.Observacao;
        //            existingTabela.PalMPV = tabela.PalMPV;
        //            existingTabela.ParceiroId = tabela.ParceiroId;
        //            existingTabela.pedido = tabela.pedido;
        //            existingTabela.Status = tabela.Status;
        //            existingTabela.TipPed = tabela.TipPed;
        //            existingTabela.TipoNegociacaoId = tabela.TipoNegociacaoId;
        //            existingTabela.Valor = tabela.Valor;
        //            existingTabela.VendedorId = tabela.VendedorId;
        //            existingTabela.Ativo = tabela.Ativo;
        //            existingTabela.Versao = tabela.Versao;


        //            _context.CabecalhoPedidoVenda.Update(existingTabela);
        //        }
        //        else
        //        {
        //            _context.CabecalhoPedidoVenda.Add(tabela);
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    return Ok(new { data = listaTabelas, message = "Pedidos de Venda criados/atualizados com sucesso." });
        //}






        [HttpPut("{id}")]

        public IActionResult Update(int id, CabecalhoPedidoVendaDto model)
        {
            _cabecalhoPedidoVendaService.Update(id, model);

            return Ok(new { message = "Pedido atualizado com sucesso" });
        }


        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id)
        {
            var pedido = _context.CabecalhoPedidoVenda.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Status = "Falhou";
            _context.SaveChanges();

            return Ok(new { message = "Erro de comunicação com o Sankya, Envio Pendente" });
        }

        [HttpPut("statusErroPalMPV")]
        public IActionResult StatusErroPalMPV([FromQuery] string PalMPV)
        {
            var pedido = _context.CabecalhoPedidoVenda.FirstOrDefault(p => p.PalMPV == PalMPV);

            if (pedido == null)
            {
                return NotFound(new { message = "Pedido não encontrado" });
            }

            pedido.Status = "Pendente";
            _context.SaveChanges();

            return Ok(new { message = "Status atualizado para Pendente" });
        }


        [HttpPut("{id}/statusErro")]
        public IActionResult UpdateStatusErro(int id)
        {
            var pedido = _context.CabecalhoPedidoVenda.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Status = "Pendente";
            _context.SaveChanges();

            return Ok(new { message = "Erro ao Enviar Pedido" });
        }

        [HttpPut("palmpv")]
        public async Task<ActionResult> UpdateInativar(List<string> palmpvList)
        {
            if (palmpvList == null || palmpvList.Count == 0)
            {
                return BadRequest("A lista de PalMPV está vazia.");
            }

            var pedidos = await _context.CabecalhoPedidoVenda
                .Where(p => palmpvList.Contains(p.PalMPV))
                .ToListAsync();

            if (pedidos == null || pedidos.Count == 0)
            {
                return BadRequest("Pedidos não encontrados.");
            }

            foreach (var pedido in pedidos)
            {
                pedido.Ativo = "N";
            }

            await _context.SaveChangesAsync();

            return Ok("Pedidos Inativados");
        }


        [HttpPut("palmpv/{palmpv}")]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> UpdatePropertyInList(string palmpv)
        {
            var pedidos = await _context.CabecalhoPedidoVenda
                .Where(p => p.PalMPV == palmpv)
                .ToListAsync();

            if (pedidos == null || pedidos.Count == 0)
            {
                return BadRequest("Pedidos não encontrados.");
            }

            foreach (var pedido in pedidos)
            {
                pedido.Ativo = "N"; 
            }

            await _context.SaveChangesAsync();

            return Ok("Propriedade Ativo atualizada com sucesso em todos os pedidos com PalMPV igual a '" + palmpv + "'.");
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

        [HttpDelete("palmpv/{palmpv}")]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> DeleteByPalMPV(string palmpv)
        {
            var pedido = await _context.CabecalhoPedidoVenda.FirstOrDefaultAsync(p => p.PalMPV == palmpv);
            if (pedido == null)
                return BadRequest("Pedido não encontrado");

            _context.CabecalhoPedidoVenda.Remove(pedido);
            await _context.SaveChangesAsync();

            return Ok("Pedido de Venda excluído com sucesso!");
        }

    }
}
