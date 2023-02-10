using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Extension;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;
using System.Text.RegularExpressions;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IVendedorService _vendedorService;

        public VendedorController(IMapper mapper, IVendedorService vendedorService, DataContext context)
        {
            _vendedorService = vendedorService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Vendedor.CountAsync();
            var data = await context.Vendedor.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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
            var total = await context.Vendedor.CountAsync();
            var vendedores = await context.Vendedor.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => (e.Nome.ToLower().Contains(filter.ToLower()) ||
                                      e.Status.ToLower().Contains(filter.ToLower())))
                         .OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = vendedores
            });
        }
        [HttpGet("promotor")]
        public async Task<IActionResult> GetAllPromotor([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] string filter

          )
        {
            var total = await context.Vendedor.CountAsync();
            var vendedores = await context.Vendedor.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                       .Where(e => (e.Tipo.ToLower().Contains(filter.ToLower())))
                         .OrderBy(e => e.Id).ToListAsync();
            return Ok(new
            {
                total,
                data = vendedores
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> Get(int id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
                return BadRequest("Vendedor não encontrado.");
            return Ok(vendedor);
        }

        //[HttpGet("{tipo}")]
        //public async Task<IActionResult> GetByTipo(string tipo)
        //{
        //    var vendedor = await _vendedorService.GetVendedoreTipoAsync(tipo);
        //    return Ok(vendedor);
        //}




        [HttpPost]
        public async Task<ActionResult<List<Vendedor>>> AddVendedor(Vendedor vendedor)
        {
            if (_context.Vendedor.Any(u => u.Id == vendedor.Id))
            {
                return BadRequest("Vendedor ja existe na base de dados.");
            }
            if (_context.Vendedor.Any(u => u.Nome == vendedor.Nome))
            {
                return BadRequest("Vendedor ja existe na base de dados.");
            }
            _context.Vendedor.Add(vendedor);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Vendedor criado com sucesso" }));
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, VendedorDto model)
        {
            _vendedorService.Update(id, model);
            return Ok(new { message = "Vendedor atualizado com sucesso" });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Vendedor>>> Delete(int id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
                return BadRequest("Vendedor não encontrado");

            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();

            return Ok("Vendedor deletado com sucesso!");
        }
    }
}
