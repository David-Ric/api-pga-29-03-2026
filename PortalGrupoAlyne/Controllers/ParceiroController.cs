using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParceiroController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IParceirosService _parceirosService;
        public ParceiroController(IMapper mapper, IParceirosService parceirosService, DataContext context) 
        {
            _parceirosService = parceirosService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Parceiro.CountAsync();
            var data = await context.Parceiro.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Parceiro
                .AsNoTracking()
                .Where(e => (e.Nome.ToLower().Contains(filter.ToLower()) ||
                 e.Cnpj_Cpf.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Parceiro
                .AsNoTracking()
               .Where(e => (e.Nome.ToLower().Contains(filter.ToLower()) ||
                e.Cnpj_Cpf.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

            
        }
        [HttpGet("filter/status")]

        public async Task<IActionResult> GetAllFilterStatus([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
         [FromQuery] string filter

         )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Parceiro
                .AsNoTracking()
                .Where(e => (e.Status.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Parceiro
                .AsNoTracking()
               .Where(e => (e.Status.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


           
        }

        [HttpGet("filter/Vendedor")]

        public async Task<IActionResult> GetAllFilterVendedor([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] int codVendedor

          )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Parceiro
                .AsNoTracking()
                 .Where(e => e.VendedorId == codVendedor)
                .OrderBy(e => e.id).Include("TabelaPrecoParceiro").Include("TabelaPrecoParceiro.Empresa").Include("TabelaPrecoParceiro.TabelaPreco").Include("Titulo")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Parceiro
                .AsNoTracking()
                .Where(e => e.VendedorId == codVendedor)
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

          
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Parceiro>> GetById(int id)
        {
            var parceiro = await _context.Parceiro
                .Include(p => p.Titulo)
                .FirstOrDefaultAsync(p => p.id == id);

            if (parceiro == null)
            {
                return NotFound();
            }

            return parceiro;
        }

       


        [HttpPost]
        public async Task<ActionResult<List<Parceiro>>> AddParceiros(Parceiro model)
        {

            if (_context.Parceiro.Any(u => u.Nome == model.Nome))
            {
                return BadRequest("Parceiro ja existe na base de dados.");
            }
            
            _context.Parceiro.Add(model);
            await _context.SaveChangesAsync();

            return Ok((new { data = model.id, message = "Parceiro criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, ParceiroDto model)
        {
            //if (_context.Produtos.Any(u => u.Nome == model.Nome))
            //{
            //    return BadRequest("Já existe um produto com essa descrição.");
            //}
            _parceirosService.Update(id, model);
            return Ok(new { message = "Parceiro atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Parceiro>>> Delete(int id)
        {
            var parceiro = await _context.Parceiro.FindAsync(id);
            if (parceiro == null)
                return BadRequest("Parceiro não encontrado");

            _context.Parceiro.Remove(parceiro);
            await _context.SaveChangesAsync();

            return Ok("Parceiro excluído com sucesso!");
        }
    }
}
