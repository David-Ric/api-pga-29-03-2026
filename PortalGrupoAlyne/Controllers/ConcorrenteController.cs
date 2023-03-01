using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConcorrenteController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IConcorrentesService _concorrenteService;

        public ConcorrenteController(IMapper mapper, IConcorrentesService concorrenteService, DataContext context)
        {
            _concorrenteService = concorrenteService;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.Concorrente.CountAsync();
            var data = await context.Concorrente.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            
            var concorrentes = await context.Concorrente.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => (e.Nome.ToLower().Contains(filter.ToLower()))).OrderBy(e => e.Id).ToListAsync();
            var total = concorrentes.Count();
            return Ok(new
            {
                total,
                data = concorrentes
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Concorrente>> Get(int id)
        {
            var concorrente = await _context.Concorrente.FindAsync(id);
            if (concorrente == null)
                return BadRequest("Concorrente não encontrado.");
            return Ok(concorrente);
        }

        [HttpPost]
        public async Task<ActionResult<List<Concorrente>>> AddProduto(Concorrente concorrente)
        {

            if (_context.Concorrente.Any(u => u.Nome == concorrente.Nome))
            {
                return BadRequest("Concorrente ja existe na base de dados.");
            }
            _context.Concorrente.Add(concorrente);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Concorrente cadastrado com sucesso" }));
        }

        [HttpPut("{id}")]
        
        public IActionResult Update(int id, ConcorrenteDto model)
        {

            //if (_context.Concorrentes.Any(u => u.Nome == model.Nome))
            //{
            //    return BadRequest("Já existe um concorrente com esse nome.");
            //}
            _concorrenteService.Update(id, model);
            return Ok(new { message = "Concorrente atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Concorrente>>> Delete(int id)
        {
            var concorrente = await _context.Concorrente.FindAsync(id);
            if (concorrente == null)
                return BadRequest("Concorrente não encontrado");

            _context.Concorrente.Remove(concorrente);
            await _context.SaveChangesAsync();

            return Ok("Concorrente excluído com sucesso!");
        }
    }
}
