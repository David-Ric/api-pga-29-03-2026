using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpcaoCampoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OpcaoCampoController(DataContext context, IMapper mapper)
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
            var total = await context.OpcaoCampo.CountAsync();
            var data = await context.OpcaoCampo.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredOpcaoCampo([FromQuery] int pagina, [FromQuery] int totalpagina, [FromQuery] string filter)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var modulos = await _context.OpcaoCampo
                .AsNoTracking()
                .Where(m => m.Opcao.ToLower().Contains(filter.ToLower()))
                .OrderBy(m => m.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await _context.OpcaoCampo
                .AsNoTracking()
                .Where(m => m.Opcao.ToLower().Contains(filter.ToLower()))
                .CountAsync();

            var modulosDto = _mapper.Map<IEnumerable<OpcaoCampoDto>>(modulos);

            return Ok(new
            {
                total,
                data = modulosDto
            });
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<OpcaoCampoDto>> GetById(int id)
        {
            var modulo = await _context.OpcaoCampo.FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }
            var moduloDto = _mapper.Map<OpcaoCampoDto>(modulo);
            return Ok(moduloDto);
        }

        [HttpGet("ByColunaModuloId/{colunaModuloId}")]
        public async Task<ActionResult<IEnumerable<OpcaoCampoDto>>> GetByColunaModuloId(int colunaModuloId)
        {
            var opcoes = await _context.OpcaoCampo
                .Where(o => o.ColunaModuloId == colunaModuloId)
                .ToListAsync();
            if (!opcoes.Any())
            {
                return NotFound();
            }
            var opcoesDto = _mapper.Map<IEnumerable<OpcaoCampoDto>>(opcoes);
            return Ok(opcoesDto);
        }


        [HttpPost]
        public async Task<ActionResult<object>> Post(OpcoesRequestDto opcoesRequestDto)
        {
            foreach (var colunaModuloId in opcoesRequestDto.ColunaModuloIds)
            {
                var existingOpcoes = await _context.OpcaoCampo.Where(o => o.ColunaModuloId == colunaModuloId).ToListAsync();
                _context.OpcaoCampo.RemoveRange(existingOpcoes);

                var opcoes = opcoesRequestDto.OpcoesDto.Where(o => o.ColunaModuloId == colunaModuloId).Select(o => _mapper.Map<OpcaoCampo>(o));
                foreach (var opcao in opcoes)
                {
                    opcao.ColunaModuloId = colunaModuloId;
                    _context.OpcaoCampo.Add(opcao);
                }
            }

            await _context.SaveChangesAsync();

            var ids = opcoesRequestDto.OpcoesDto.Select(o => o.Id);

            return new
            {
                Ids = ids
            };
        }


       







        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OpcaoCampoDto moduloDto)
        {
            if (id != moduloDto.Id)
            {
                return BadRequest();
            }
            var modulo = _mapper.Map<OpcaoCampo>(moduloDto);
            _context.Entry(modulo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.OpcaoCampo.Any(m => m.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }


        [HttpDelete("colunamodulo/{id}")]
        public async Task<IActionResult> DeleteByColunaModuloId(int id)
        {
            var opcao = await _context.OpcaoCampo.Where(l => l.ColunaModuloId == id).ToListAsync();
            if (opcao == null)
            {
                return NotFound();
            }
            _context.OpcaoCampo.RemoveRange(opcao);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var modulo = await _context.OpcaoCampo.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            _context.OpcaoCampo.Remove(modulo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("colunaModulo/{colunaModuloId}/campo/{nomeCampo}")]
        public async Task<IActionResult> Delete(int colunaModuloId, string nomeCampo)
        {
            var opcaoCampo = await _context.OpcaoCampo
                .Where(o => o.ColunaModuloId == colunaModuloId && o.Opcao == nomeCampo)
                .FirstOrDefaultAsync();
            if (opcaoCampo == null)
            {
                return NotFound();
            }

            _context.OpcaoCampo.Remove(opcaoCampo);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
