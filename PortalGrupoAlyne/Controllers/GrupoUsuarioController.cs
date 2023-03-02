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
    public class GrupoUsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IUserGrupoService _userGrupoService;

        public GrupoUsuarioController(DataContext context, IMapper mapper, IUserGrupoService userGrupoService)
        {
            _userGrupoService = userGrupoService;
            _context = context;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.GrupoUsuario.CountAsync();
            var data = await context.GrupoUsuario.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("filter")]

        public async Task<IActionResult> GetGrupoFilter([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina,
             [FromQuery] string filter
            )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.GrupoUsuario
                .AsNoTracking()
                .Where(e => (e.Nome.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.GrupoUsuario
                .AsNoTracking()
                .Where(e => (e.Nome.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var grupo = await _userGrupoService.GetUserGrupoByIdAsync(id);
                if (grupo == null) return NoContent();

                return Ok(grupo);
            }
            catch (Exception ex)
            {
                return BadRequest("Grupo de usuário não encontrado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<GrupoUsuario>>> AddTabelaPreco(GrupoUsuario grupo)
        {

            if (_context.GrupoUsuario.Any(u => u.Nome == grupo.Nome))
            {
                return BadRequest("Grupo de usuário ja existe na base de dados.");
            }
           
            _context.GrupoUsuario.Add(grupo);
            await _context.SaveChangesAsync();

            return Ok((new { data = grupo.Id, message = "Grupo de usuários cadastrado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, GrupoUsuarioDto model)
        {
            _userGrupoService.Update(id, model);
            return Ok(new { message = "Grupo de usuários atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GrupoUsuario>>> Delete(int id)
        {
            var grupo = await _context.GrupoUsuario.FindAsync(id);
            if (grupo == null)
                return BadRequest("Grupo de usuarios não encontrado");

            _context.GrupoUsuario.Remove(grupo);
            await _context.SaveChangesAsync();

            return Ok("Grupo de usuários com sucesso!");
        }
    }
}