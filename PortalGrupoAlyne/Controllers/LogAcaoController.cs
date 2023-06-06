using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogAcaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IUserGrupoService _userGrupoService;

        public LogAcaoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.LogAcao.CountAsync();
            var data = await context.LogAcao.AsNoTracking()
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina)
                .ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter/Usuario")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilterUsiario([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina,
             [FromQuery] string filter
            )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.LogAcao
                .AsNoTracking()
                .Where(e => (e.UserName.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.LogAcao
                .AsNoTracking()
                .Where(e => (e.UserName.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

        }

        [HttpGet("filter/Metodo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilterMetodo([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina,
             [FromQuery] string filter
            )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.LogAcao
                .AsNoTracking()
                .Where(e => (e.Metodo.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.LogAcao
                .AsNoTracking()
                .Where(e => (e.Metodo.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

        }

        [HttpGet("filter/Tabela")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilterTabela([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
           [FromQuery] string filter
          )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.LogAcao
                .AsNoTracking()
                .Where(e => (e.Tabela.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.LogAcao
                .AsNoTracking()
                .Where(e => (e.Tabela.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

        }

        [HttpGet("filter/Data")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilterData([FromServices] DataContext context,
     [FromQuery] int pagina,
     [FromQuery] int totalpagina,
     [FromQuery] DateTime filter)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.LogAcao
                .AsNoTracking()
                .Where(e => e.Data == filter.Date)
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.LogAcao
                .AsNoTracking()
                .Where(e => e.Data == filter.Date)
                .CountAsync();

            return Ok(new
            {
                total,
                data
            });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LogAcao>> GetLogId(int id)
        {
            var grupo = await _context.LogAcao.FindAsync(id);
            if (grupo == null)
                return BadRequest("Grupo não encontrado.");
            return Ok(grupo);
        }



        [HttpPost]
        public async Task<ActionResult<List<LogAcao>>> AddLogAcao(LogAcao Log)
        {

            if (_context.LogAcao.Any(u => u.Id == Log.Id))
            {
                return BadRequest("Log ja existe na base de dados.");
            }

            _context.LogAcao.Add(Log);
            await _context.SaveChangesAsync();

            return Ok((new { data = Log.Id, message = "Log registrado com sucesso" }));
        }


        [HttpPatch("{id}")]
        public IActionResult AtualizarObs(int id, [FromBody] string obs)
        {
            // Recupera o LogAcao do banco de dados
            var logAcao = _context.LogAcao.FirstOrDefault(l => l.Id == id);

            if (logAcao != null)
            {
                logAcao.Obs = obs;

                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<List<LogAcao>>> Delete(int id)
        {
            var log = await _context.LogAcao.FindAsync(id);
            if (log == null)
                return BadRequest("Log não encontrado");

            _context.LogAcao.Remove(log);
            await _context.SaveChangesAsync();

            return Ok("log excluído com sucesso!");
        }
    }
}
