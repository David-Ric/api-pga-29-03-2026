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
            var data = await context.CabecalhoPedidoVenda.Include("Vendedor").Include("Parceiro").Include("TipoNegociacao").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            
            var data = await context.CabecalhoPedidoVenda.Where(e => e.Vendedor.Id == codVendedor).OrderBy(e => e.Id).Include("Vendedor").Include("Parceiro").Include("TipoNegociacao").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();
            var total = data.Count();
            return Ok(new
            {
                total,
                data = data
            });
        }
        //[HttpGet("Pendentes")]
        //public async Task<IActionResult> GetPendentes([FromServices] DataContext context)
        //{
        //    var header = await context.CabecalhoPedidoVenda
        //        .Where(h => h.Status == "Pendente")
        //        .FirstOrDefaultAsync();

        //    if (header == null)
        //    {
        //        return NotFound("Não há cabeçalhos de venda pendentes");
        //    }

        //    var items = await context.ItemPedidoVenda
        //        .Where(i => i.PalMPV == header.PalMPV)
        //        .ToListAsync();

        //    var result = new
        //    {
        //        CabecalhoPedidoVenda = header,
        //        ItemPedidoVenda = items
        //    };

        //    return Ok(result);
        //}




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

            if (_context.CabecalhoPedidoVenda.Any(u => u.Id == tabela.Id))
            {
                return BadRequest("Pedido de Venda ja existe na base de dados.");
            }


            _context.CabecalhoPedidoVenda.Add(tabela);
            await _context.SaveChangesAsync();

            return Ok((new {data=tabela, message = "Pedido de Venda criado com sucesso." }));
        }

        //============salvar diversas tabelas =======================================================================
        [HttpPost("Lista")]
        public async Task<ActionResult<List<CabecalhoPedidoVenda>>> AddPedido(List<CabecalhoPedidoVenda> listaTabelas)
        {
            List<CabecalhoPedidoVenda> tabelasJaExistem = new List<CabecalhoPedidoVenda>();

            foreach (var tabela in listaTabelas)
            {
                if (_context.CabecalhoPedidoVenda.Any(u => u.Id == tabela.Id))
                {
                    tabelasJaExistem.Add(tabela);
                }
                else
                {
                    _context.CabecalhoPedidoVenda.Add(tabela);
                }
            }

            if (tabelasJaExistem.Count > 0)
            {
                return BadRequest("Os seguintes Pedidos de Venda já existem na base de dados: " +
                    string.Join(", ", tabelasJaExistem.Select(t => t.Id.ToString())));
            }

            await _context.SaveChangesAsync();

            return Ok(new { data = listaTabelas, message = "Pedidos de Venda criados com sucesso." });
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

        [HttpPut("{id}/statusErro")]
        public IActionResult UpdateStatusErro(int id)
        {
            var pedido = _context.CabecalhoPedidoVenda.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Status = "Não Enviado";
            _context.SaveChanges();

            return Ok(new { message = "Erro ao Enviar Pedido" });
        }

        [HttpPut("{palMPV}/statusErroPalMPV")]
        public IActionResult UpdateStatusErroPalMPV(string palMPV)
        {
            var pedido = _context.CabecalhoPedidoVenda.FirstOrDefault(p => p.PalMPV == palMPV);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Status = "Não Enviado";
            _context.SaveChanges();

            return Ok(new { message = "Erro ao Enviar Pedido" });
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
