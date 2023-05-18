using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;
using System.Text.RegularExpressions;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaPrecoParceiroController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly ITabelaPrecoParceiroService _tabelaPrecoParceiroService;
        public TabelaPrecoParceiroController(DataContext context, IMapper mapper, ITabelaPrecoParceiroService tabelaPrecoParceiroService)
        {
            _tabelaPrecoParceiroService = tabelaPrecoParceiroService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.TabelaPrecoParceiro.CountAsync();
            var data = await context.TabelaPrecoParceiro.Include(e => e.TabelaPreco).Include(e => e.Empresa).AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter/cliente/empresa")]
        public async Task<IActionResult> GetAllFilterCleinteEmpresa([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int? codCliente,
            [FromQuery] int? codEmpresa
           )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.TabelaPrecoParceiro
            .AsNoTracking()
                 .Where(e => (e.ParceiroId == codCliente) && (e.EmpresaId == codEmpresa))
                .OrderBy(e => e.id).Include(e => e.TabelaPreco).Include(e => e.Empresa)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.TabelaPrecoParceiro
            .AsNoTracking()
                .Where(e => (e.ParceiroId == codCliente) && (e.EmpresaId == codEmpresa))
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
                var tabela = await _tabelaPrecoParceiroService.GetTabelaClienteAsync(id);
                if (tabela == null) return NoContent();

                return Ok(tabela);
            }
            catch (Exception ex)
            {
                return BadRequest("Tabela de preço não encontrada.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<TabelaPrecoParceiro>>> AddTabela(TabelaPrecoParceiro tabela)
        {

            if (_context.TabelaPrecoParceiro.Any(u => u.id == tabela.id))
            {
                return BadRequest("Esta tabela ja existe na base de dados.");
            }
            if (_context.TabelaPrecoParceiro.Any(u => u.ParceiroId == tabela.ParceiroId && u.EmpresaId ==tabela.EmpresaId))
            {
                return BadRequest("Este Parceiro Ja tem ligação com esta empresa.");
            }
            if (_context.TabelaPrecoParceiro.Any(u => u.ParceiroId == tabela.ParceiroId && u.TabelaPrecoId == tabela.TabelaPrecoId))
            {
                return BadRequest("Este Parceiro Ja tem ligação com esta tabela de preço.");
            }
            _context.TabelaPrecoParceiro.Add(tabela);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Tabela de preço criada com sucesso." }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, TabelaPrecoParceiroDto model)
        {
            _tabelaPrecoParceiroService.Update(id, model);
            return Ok(new { message = "Tabela de preço atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TabelaPrecoParceiro>>> Delete(int id)
        {
            var tabela = await _context.TabelaPrecoParceiro.FindAsync(id);
            if (tabela == null)
                return BadRequest("Tabela de preço não encontrada");

            _context.TabelaPrecoParceiro.Remove(tabela);
            await _context.SaveChangesAsync();

            return Ok("Tabela de preço excluída com sucesso!");
        }
    }
}
