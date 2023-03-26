using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunicadoComercialController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public ComunicadoComercialController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromServices] DataContext context)
        //{

        //    var data = await context.ComunicadoComercial.AsNoTracking().ToListAsync();

        //    return Ok(data);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.ComunicadoComercial.CountAsync();
            var data = await context.ComunicadoComercial
                .AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina)
                .ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter/titulo")]
        public async Task<IActionResult> GetAllFilter([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
           [FromQuery] string? titulo

           )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ComunicadoComercial
                .AsNoTracking()
                .Where(e => (e.Titulo.ToLower().Contains(titulo.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ComunicadoComercial
                .AsNoTracking()
                  .Where(e => (e.Titulo.ToLower().Contains(titulo.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


        }
        [HttpGet("filter/grupo")]
        public async Task<IActionResult> GetAllFilterGrupo([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] int? grupoId

          )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ComunicadoComercial
                 .AsNoTracking()
                .Where(e => (e.GrupoId == grupoId))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ComunicadoComercial
                 .AsNoTracking()
                .Where(e => (e.GrupoId == grupoId))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] DataContext context, int id)
        {
            var comunicado = await context.ComunicadoComercial
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (comunicado == null)
            {
                return NotFound();
            }

            return Ok(comunicado);
        }


        [HttpPost]
        public async Task<ActionResult<List<ComunicadoComercial>>> AddComunicado(ComunicadoComercial comunicado)
        {

            if (_context.ComunicadoComercial.Any(u => u.Id == comunicado.Id))
            {
                return BadRequest("Este comunicado ja existe na base de dados.");
            }
            _context.ComunicadoComercial.Add(comunicado);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Comunicado criado com sucesso." }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ComunicadoComercial comunicado)
        {
            if (id != comunicado.Id)
            {
                return BadRequest();
            }
            _context.Entry(comunicado).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ComunicadoComercial.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var postLido = _context.ComunicadoComercial.Find(id);

                if (postLido == null)
                {
                    return NotFound("Comunicado não encontrado!");
                }

                _context.ComunicadoComercial.Remove(postLido);
                _context.SaveChanges();

                return Ok("Comunicado removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover Post Lido: {ex.Message}");
            }
        }
    }
}
