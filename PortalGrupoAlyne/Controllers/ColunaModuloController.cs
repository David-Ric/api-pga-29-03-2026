using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColunaModuloController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ColunaModuloController(DataContext context, IMapper mapper)
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
            var total = await context.ColunaModulo.CountAsync();
            var data = await context.ColunaModulo.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredColunaModulos([FromQuery] int pagina, [FromQuery] int totalpagina, [FromQuery] string filter)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var modulos = await _context.ColunaModulo
                .AsNoTracking()
                .Where(m => m.Nome.ToLower().Contains(filter.ToLower()))
                .OrderBy(m => m.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await _context.ColunaModulo
                .AsNoTracking()
                .Where(m => m.Nome.ToLower().Contains(filter.ToLower()))
                .CountAsync();

            var modulosDto = _mapper.Map<IEnumerable<ColunaModuloDto>>(modulos);

            return Ok(new
            {
                total,
                data = modulosDto
            });
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<ColunaModuloDto>> GetById(int id)
        {
            var modulo = await _context.ColunaModulo.Include(m => m.OpcaoCampo).FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }
            var moduloDto = _mapper.Map<ColunaModuloDto>(modulo);
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
        public async Task<ActionResult<object>> Post(ColunaModuloDto moduloDto)
        {
            var modulo = _mapper.Map<ColunaModulo>(moduloDto);
            _context.ColunaModulo.Add(modulo);
            await _context.SaveChangesAsync();
            moduloDto.Id = modulo.Id;

            return new
            {
                Id = modulo.Id
            };
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ColunaModuloDto moduloDto)
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
                if (!_context.ColunaModulo.Any(m => m.Id == id))
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
            var modulo = await _context.ColunaModulo.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            _context.ColunaModulo.Remove(modulo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
