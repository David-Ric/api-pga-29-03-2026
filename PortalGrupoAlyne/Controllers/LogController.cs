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
    public class LogController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public LogController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {
            var data = await context.Logs
                                    .AsNoTracking()
                                    .OrderByDescending(l => l.Id)
                                    .ToListAsync();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Logs>> GetById(int id)
        {
            var log = await _context.Logs.FindAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return (log);
        }


        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<LogsDto> logsDTO)
        {
            try
            {
                var logs = _mapper.Map<IEnumerable<Logs>>(logsDTO);

                foreach (var log in logs)
                {
                    _context.Logs.Add(log);
                }

                _context.SaveChanges();

                return Ok("Log adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar Log: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Logs logs)
        {
            if (id != logs.Id)
            {
                return BadRequest();
            }
            _context.Entry(logs).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Logs.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }
    }
}
