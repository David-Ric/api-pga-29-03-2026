using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IProdutoService _produtoService;

        public ProdutoController(IMapper mapper, IProdutoService produtoService, DataContext context)
        {
            _produtoService = produtoService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
      
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Produto.CountAsync();
            var data = await context.Produto.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).Include("GrupoProduto").ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
        [HttpGet("total")]

        public async Task<IActionResult> GetAlltotal([FromServices] DataContext context)
        {
            var total = await context.Produto.CountAsync();
            var data = await context.Produto.AsNoTracking().Include("GrupoProduto").ToListAsync();

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
           [FromQuery] string? filter
       

           )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Produto
                .AsNoTracking()
                 .Where(e => (e.Nome.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id).Include("GrupoProduto")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Produto
                .AsNoTracking()
                .Where(e => (e.Nome.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

            
        }
        [HttpGet("filter/grupo")]

        public async Task<IActionResult> GetAllGrupo([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
          [FromQuery] int? grupo

          )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Produto
                .AsNoTracking()
                 .Where(e => e.GrupoProdutoId == grupo)
                .OrderBy(e => e.Id).Include("GrupoProduto")
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Produto
                .AsNoTracking()
                .Where(e => e.GrupoProdutoId == grupo)
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
                var produto = await _produtoService.GetProdutoId(id);
                if (produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest("Produto não encontrado.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<Produto>>> AddProduto(Produto produto)
        {
            if (_context.Produto.Any(u => u.Id == produto.Id))
            {
                return BadRequest("Código do produto ja existe na base de dados.");
            }

            if (_context.Produto.Any(u => u.Nome == produto.Nome))
            {
                return BadRequest("Nome do produto ja existe na base de dados.");
            }
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Produto criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, ProdutoDto model)
        {
            _produtoService.Update(id, model);
            return Ok(new { message = "Produto atualizado com sucesso" });
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Produto>>> Delete(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
                return BadRequest("Pruduto não encontrado");

            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok("Produto excluído com sucesso!");
        }
    }
}
