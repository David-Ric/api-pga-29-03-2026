using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoNegociacaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private ITipoNegociacaoService _tipoNegociacaoService;

        public TipoNegociacaoController(DataContext context, IMapper mapper, ITipoNegociacaoService tipoNegociacaoService)
        {
            _tipoNegociacaoService= tipoNegociacaoService;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.TipoNegociacao.CountAsync();
            var data = await context.TipoNegociacao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter")]

        public async Task<IActionResult> GetAllFilter([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
           [FromQuery] string filter

           )
        {
            var total = await context.TipoNegociacao.CountAsync();
            var negociacoes = await context.TipoNegociacao.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => (e.Descricao.ToLower().Contains(filter.ToLower())))
                         .OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = negociacoes
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoNegociacao>> Get(int id)
        {
            var negociacao = await _context.TipoNegociacao.FindAsync(id);
            if (negociacao == null)
                return BadRequest("Tipo de negociação não encontrada.");
            return Ok(negociacao);
        }

        [HttpPost]
        public async Task<ActionResult<List<TipoNegociacao>>> AddProduto(TipoNegociacao request)
        {

            if (_context.TipoNegociacao.Any(u => u.Descricao == request.Descricao))
            {
                return BadRequest("Tipo de negociação ja existe na base de dados.");
            }
            _context.TipoNegociacao.Add(request);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Tipo de negociação cadastrada com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, TipoNegociacaoDto model)
        {
            _tipoNegociacaoService.Update(id, model);
            return Ok(new { message = "Tipo de negociação atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TipoNegociacao>>> Delete(int id)
        {
            var negociacao = await _context.TipoNegociacao.FindAsync(id);
            if (negociacao == null)
            return BadRequest("Tipo de negociação criada com sucesso");

            _context.TipoNegociacao.Remove(negociacao);
            await _context.SaveChangesAsync();

            return Ok("Tipo de negociação criada com sucesso");
        }
    }
}
