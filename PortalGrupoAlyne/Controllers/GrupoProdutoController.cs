using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoProdutoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        public GrupoProdutoController(IMapper mapper, DataContext context)
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
            var total = await context.GrupoProduto.CountAsync();
            var data = await context.GrupoProduto.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            [FromQuery] string Nome_Grupo
        
            )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.GrupoProduto
                .AsNoTracking()
                .Where(e => (e.Nome.ToLower().Contains(Nome_Grupo.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.GrupoProduto
                .AsNoTracking()
                .Where(e => (e.Nome.ToLower().Contains(Nome_Grupo.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoProduto>> Get(int id)
        {
            var grupo = await _context.GrupoProduto.FindAsync(id);
            if (grupo == null)
                return BadRequest("Grupo não encontrado.");
            return Ok(grupo);
        }



        [HttpPost]
        public async Task<ActionResult<List<GrupoProduto>>> AddGrupo(GrupoProduto grupo)
        {
            if (_context.GrupoProduto.Any(u => u.Nome == grupo.Nome))
            {
                return BadRequest("Grupo ja existe na base de dados.");
            }
            _context.GrupoProduto.Add(grupo);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Grupo criado com sucesso" }));
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<List<GrupoProduto>>> UpdateGrupo(GrupoProduto request)
        {
            var grupo = await _context.GrupoProduto
                .FindAsync(request.Id);
            if (grupo == null)
                return BadRequest("Grupo não encontrado.");

            //if (_context.Grupos.Any(u => u.nameGrupo == request.nameGrupo))
            //{
            //    return BadRequest("Já existe um grupo com esse nome.");
            //}
            grupo.Nome = request.Nome;

            await _context.SaveChangesAsync();

            return Ok(new { message = "grupo atualizado com sucesso" });
        }


     
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GrupoProduto>>> Delete(int id)
        {
            var grupo = await _context.GrupoProduto.FindAsync(id);
            if (grupo == null)
                return BadRequest("Grupo não encontrado");

            _context.GrupoProduto.Remove(grupo);
            await _context.SaveChangesAsync();

            return Ok("Grupo deletado com sucesso!");
        }
    }
}
