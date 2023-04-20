using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ModuloController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.Modulo.CountAsync();
            var data = await context.Modulo.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredModulos([FromQuery] int pagina, [FromQuery] int totalpagina, [FromQuery] string filter)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var modulos = await _context.Modulo.Include(m => m.ColunaModulo)
                .AsNoTracking()
                .Where(m => m.Descricao.ToLower().Contains(filter.ToLower()))
                .OrderBy(m => m.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await _context.Modulo
                .AsNoTracking()
                .Where(m => m.Descricao.ToLower().Contains(filter.ToLower()))
                .CountAsync();

            var modulosDto = _mapper.Map<IEnumerable<ModuloDto>>(modulos);

            return Ok(new
            {
                total,
                data = modulosDto
            });
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<ModuloDto>> GetById(int id)
        {
            var modulo = await _context.Modulo.Include(m => m.ColunaModulo).Include(m=>m.LigacaoTabela).FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }
            var moduloDto = _mapper.Map<ModuloDto>(modulo);
            return Ok(moduloDto);
        }

        //[HttpPost]
        //public async Task<ActionResult<ModuloDto>> Post(ModuloDto moduloDto)
        //{
        //    var modulo = _mapper.Map<Modulo>(moduloDto);
        //    _context.Modulo.Add(modulo);
        //    await _context.SaveChangesAsync();
        //    moduloDto.Id = modulo.Id;
        //    return CreatedAtAction(nameof(GetById), new { id = modulo.Id }, moduloDto);
        //}

        [HttpPost]
        public async Task<ActionResult<object>> Post(ModuloDto moduloDto)
        {
            var modulo = _mapper.Map<Modulo>(moduloDto);
            _context.Modulo.Add(modulo);
            await _context.SaveChangesAsync();
            moduloDto.Id = modulo.Id;

            return new
            {
                Id = modulo.Id
            };
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ModuloDto moduloDto)
        {
            if (id != moduloDto.Id)
            {
                return BadRequest();
            }
            var modulo = _mapper.Map<Modulo>(moduloDto);
            _context.Entry(modulo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Modulo.Any(m => m.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var modulo = await _context.Modulo.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            _context.Modulo.Remove(modulo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
