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
    public class ProdutoConcorrenteController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IProdutoConcorrenteService _produtoConcorrenteService;
        public ProdutoConcorrenteController(IMapper mapper,IProdutoConcorrenteService produtoConcorrenteService, DataContext context)
        {
            _produtoConcorrenteService= produtoConcorrenteService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina
          )
        {
            var total = await context.ProdutoConcorrente.CountAsync();
            var data = await context.ProdutoConcorrente.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            
            var prodconcorrentes = await context.ProdutoConcorrente.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                       .Where(e => (e.NomeProduto.ToLower().Contains(filter.ToLower()) ||
                                      e.NomeProdutoSimilar.ToLower().Contains(filter.ToLower())))
                         .OrderBy(e => e.Id).ToListAsync();
            var total = prodconcorrentes.Count();
            return Ok(new
            {
                total,
                data = prodconcorrentes
            });
        }
       
        [HttpGet("concorrente")]

        public async Task<IActionResult> GetAllConcorrente([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] string filter

          )
        {
            
            var prodconcorrentes = await context.ProdutoConcorrente.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                       .Where(e => (e.NomeConcorrente.ToLower().Contains(filter.ToLower())))
                         .OrderBy(e => e.Id).ToListAsync();
            var total = prodconcorrentes.Count();

            return Ok(new
            {
                total,
                data = prodconcorrentes
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoConcorrente>> Get(int id)
        {
            var prodconcorrente = await _context.ProdutoConcorrente.FindAsync(id);
            if (prodconcorrente == null)
                return BadRequest("Produto concorrente não encontrado.");
            return Ok(prodconcorrente);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProdutoConcorrente>>> AddProduto(ProdutoConcorrente prodConcorrente)
        {

            if (_context.ProdutoConcorrente.Any(u =>u.Id==prodConcorrente.Id && u.NomeProduto == prodConcorrente.NomeProduto))
            {
                return BadRequest("Produto ja existe na base de dados.");
            }
            if (_context.ProdutoConcorrente.Any(u => u.NomeProdutoSimilar == prodConcorrente.NomeProdutoSimilar))
            {
                return BadRequest("Produto similar ja existe na base de dados.");
            }
            _context.ProdutoConcorrente.Add(prodConcorrente);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Produto x Concorrente cadastrado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, ProdutoConcorrenteDto model)
        {


            //if (_context.ProdutoConcorrente.Any(u => u.NomeProduto == model.NomeProduto))
            //{
            //    return BadRequest("Produto ja existe na base de dados.");
            //}
            //if (_context.ProdutoConcorrente.Any(u => u.NomeProdutoSimilar == model.NomeProdutoSimilar))
            //{
            //    return BadRequest("Produto similar ja existe na base de dados.");
            //}
            _produtoConcorrenteService.Update(id, model);
            return Ok(new { message = "Produto similar atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProdutoConcorrente>>> Delete(int id)
        {
            var prodconcorrente = await _context.ProdutoConcorrente.FindAsync(id);
            if (prodconcorrente == null)
                return BadRequest("Registro não encontrado");

            _context.ProdutoConcorrente.Remove(prodconcorrente);
            await _context.SaveChangesAsync();

            return Ok("Produto similar excluído com sucesso!");
        }


    }
}
