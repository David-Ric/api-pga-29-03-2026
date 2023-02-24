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
    public class EmpresaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly IEmpresaService _empresaService;
        public EmpresaController(DataContext context, IMapper mapper,IEmpresaService empresaService)
        {
            _empresaService= empresaService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Empresa.CountAsync();
            var data = await context.Empresa.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
          [FromQuery] string filter

          )
        {
            var total = await context.Empresa.CountAsync();
            var negociacoes = await context.Empresa.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => (e.Descricao.ToLower().Contains(filter.ToLower())))
                         .OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = negociacoes
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> Get(int id)
        {
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
                return BadRequest("Empresa não encontrada.");
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<List<Empresa>>> AddEmpresa(Empresa empresa)
        {

            if (_context.Empresa.Any(u => u.Descricao == empresa.Descricao))
            {
                return BadRequest("Esta empresa ja existe na base de dados.");
            }
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Empresa criada com sucesso." }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, EmpresaDto model)
        {
            _empresaService.Update(id, model);
            return Ok(new { message = "Empresa atualizada com sucesso" });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Empresa>>> Delete(int id)
        {
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
                return BadRequest("Empresa não encontrada");
            if (_context.TabelaPrecoParceiro.Any(u => u.EmpresaId == empresa.Id))
            {
                return BadRequest("Esta empresa não pode ser excluída, pois está associada a um parceiro.");
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return Ok("Empresa excluída com sucesso!");
        }
    }
}
