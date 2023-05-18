using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LigacaoTabelaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LigacaoTabelaController(DataContext context, IMapper mapper)
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
            var total = await context.LigacaoTabela.CountAsync();
            var data = await context.LigacaoTabela.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("ModuloId")]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int ModuloId
           )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var modulos = await _context.LigacaoTabela
                .AsNoTracking()
                .Where(m => m.ModuloId==ModuloId)
                .OrderBy(m => m.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await _context.LigacaoTabela
                .AsNoTracking()
                 .Where(m => m.ModuloId == ModuloId)
                .CountAsync();

            var modulosDto = _mapper.Map<IEnumerable<LigacaoTabelaDto>>(modulos);

            return Ok(new
            {
                total,
                data = modulosDto
            });
        }

        




        [HttpGet("{id}")]
        public async Task<ActionResult<LigacaoTabelaDto>> GetById(int id)
        {
            var modulo = await _context.LigacaoTabela.FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }
            var moduloDto = _mapper.Map<LigacaoTabelaDto>(modulo);
            return Ok(moduloDto);
        }




       
        [HttpPost("{moduloId}")]
        public async Task<ActionResult<object>> Post(int moduloId, IEnumerable<LigacaoTabelaDto> ligacoesDto)
        {
            var existingLigacoes = await _context.LigacaoTabela.Where(l => l.ModuloId == moduloId).ToListAsync();
            _context.LigacaoTabela.RemoveRange(existingLigacoes); // delete all existing connections

            var ligacoes = _mapper.Map<IEnumerable<LigacaoTabela>>(ligacoesDto);
            foreach (var ligacao in ligacoes)
            {
                ligacao.ModuloId = moduloId; // set the module ID for each new connection
                _context.LigacaoTabela.Add(ligacao);
            }

            await _context.SaveChangesAsync();

            var ids = ligacoes.Select(l => l.Id);

            return new
            {
                Ids = ids
            };
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LigacaoTabelaDto moduloDto)
        {
            if (id != moduloDto.Id)
            {
                return BadRequest();
            }
            var modulo = _mapper.Map<LigacaoTabela>(moduloDto);
            _context.Entry(modulo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.LigacaoTabela.Any(m => m.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("modulo/{id}")]
        public async Task<IActionResult> DeleteByModuloId(int id)
        {
            var ligacoes = await _context.LigacaoTabela.Where(l => l.ModuloId == id).ToListAsync();
            if (ligacoes == null)
            {
                return NotFound();
            }
            _context.LigacaoTabela.RemoveRange(ligacoes);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var modulo = await _context.LigacaoTabela.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            _context.LigacaoTabela.Remove(modulo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
