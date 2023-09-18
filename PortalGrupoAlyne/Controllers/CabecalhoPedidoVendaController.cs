using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CabecalhoPedidoVendaController(DataContext context, IMapper mapper, ICabecalhoPedidoVendaService cabecalhoPedidoVendaService)
        {
            _cabecalhoPedidoVendaService = cabecalhoPedidoVendaService;
            _context = context;
            _mapper = mapper;
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
                var cabecalho = await _cabecalhoPedidoVendaService.GetPedidoVendaByIdAsync(id);
                if (cabecalho == null) return NoContent();

                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                return BadRequest("Pedido não encontrada.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> AddPedido(CabecalhoPedidoVenda tabela)
        {
            if (tabela.Valor <= 0)
            {
                return BadRequest("O Valor do Pedido não pode ser igual a zero.");
            }

            var existingPedido = await _context.CabecalhoPedidoVenda.FirstOrDefaultAsync(u => u.PalMPV == tabela.PalMPV);

            if (existingPedido != null)
            {

                existingPedido.Data = tabela.Data;
                existingPedido.DataEntrega = tabela.DataEntrega;
                existingPedido.Filial = tabela.Filial;
                existingPedido.Observacao = tabela.Observacao;
                existingPedido.PalMPV = tabela.PalMPV;
                existingPedido.ParceiroId = tabela.ParceiroId;
                existingPedido.pedido = tabela.pedido;
                existingPedido.Status = tabela.Status;
                existingPedido.TipPed = tabela.TipPed;
                existingPedido.TipoNegociacaoId = tabela.TipoNegociacaoId;
                existingPedido.Valor = tabela.Valor;
                existingPedido.VendedorId = tabela.VendedorId;
              

                _context.CabecalhoPedidoVenda.Update(existingPedido);
                await _context.SaveChangesAsync();

                return Ok(new { data = existingPedido, message = "Pedido de Venda existente atualizado com sucesso." });
            }

            _context.CabecalhoPedidoVenda.Add(tabela);
            await _context.SaveChangesAsync();

            return Ok(new { data = tabela, message = "Pedido de Venda criado com sucesso." });
        }


        
        
        [HttpPost("Lista")]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> AddOrUpdatePedido(List<CabecalhoPedidoVenda> listaTabelas)
        {
            foreach (var tabela in listaTabelas)
            {
                var existingTabela = await _context.CabecalhoPedidoVenda.FirstOrDefaultAsync(u => u.PalMPV == tabela.PalMPV);

                if (existingTabela != null)
                {
                    // Atualize manualmente as propriedades do objeto existente
                    existingTabela.Data = tabela.Data;
                    existingTabela.DataEntrega = tabela.DataEntrega;
                    existingTabela.Filial = tabela.Filial;
                    existingTabela.Observacao = tabela.Observacao;
                    existingTabela.PalMPV = tabela.PalMPV;
                    existingTabela.ParceiroId = tabela.ParceiroId;
                    existingTabela.pedido = tabela.pedido;
                    existingTabela.Status = tabela.Status;
                    existingTabela.TipPed = tabela.TipPed;
                    existingTabela.TipoNegociacaoId = tabela.TipoNegociacaoId;
                    existingTabela.Valor = tabela.Valor;
                    existingTabela.VendedorId = tabela.VendedorId;

                    // Atualize a entidade no contexto
                    _context.CabecalhoPedidoVenda.Update(existingTabela);
                }
                else
                {
                    _context.CabecalhoPedidoVenda.Add(tabela);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { data = listaTabelas, message = "Pedidos de Venda criados/atualizados com sucesso." });
        }






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

            pedido.Status = "Pendente";
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
