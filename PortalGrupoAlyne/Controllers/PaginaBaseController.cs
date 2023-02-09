using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaBaseController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IPaginaBaseService _paginaBaseService;

        public PaginaBaseController(IMapper mapper, IPaginaBaseService paginaBaseService, DataContext context)
        {
            _paginaBaseService = paginaBaseService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.PaginaBase.CountAsync();
            var data = await context.PaginaBase.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("Get-Nome")]
        public async Task<IActionResult> GetAllName([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
           [FromQuery] string Nome

           )
        {
            var total = await context.PaginaBase.CountAsync();
            var grupos = await context.PaginaBase.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => (e.Nome.ToLower().Contains(Nome.ToLower()))).OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = grupos
            });
        }
        [HttpGet("Get-Codigo")]
        public async Task<IActionResult> GetAllCodigo([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] int Codigo

          )
        {
            var total = await context.PaginaBase.CountAsync();
            var grupos = await context.PaginaBase.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                  .Where(e => e.Codigo==Codigo).OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = grupos
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaginaBase>> Get(int id)
        {
            var pagina = await _context.PaginaBase.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada.");
            return Ok(pagina);
        }

        [HttpPost]
        public async Task<ActionResult<List<PaginaBase>>> AddProduto(PaginaBase pagina)
        {

            if (_context.PaginaBase.Any(u => u.Nome == pagina.Nome))
            {
                return BadRequest("Página ja existe na base de dados.");
            }
            _context.PaginaBase.Add(pagina);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Página criada com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, PaginaBaseDto model)
        {
            _paginaBaseService.Update(id, model);
            return Ok(new { message = "Página atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PaginaBase>>> Delete(int id)
        {
            var pagina = await _context.PaginaBase.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada");

            _context.PaginaBase.Remove(pagina);
            await _context.SaveChangesAsync();

            return Ok("Página excluída com sucesso!");
        }

    }
}
