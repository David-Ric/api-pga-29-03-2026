using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTabelaPrecoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IItemTabelaService _itemTabelaService;
        public ItemTabelaPrecoController(DataContext context, IMapper mapper, IItemTabelaService itemTabelaService)
        {
            _itemTabelaService = itemTabelaService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {

            var total = await context.ItemTabela.CountAsync();
            var data = await context.ItemTabela.AsNoTracking().Include(i => i.Produtos).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        

        [HttpGet("codTabela/codProduto")]
        public async Task<IActionResult> GetAllFilteritemcodProd([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
           [FromQuery] int? codTabela,
           [FromQuery] int? codProduto

          )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
               .Where(e => (e.TabelaPrecoId == codTabela) && (e.IdProd == codProduto))
                .OrderBy(e => e.Id).Include(e => e.Produtos)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemTabela
                .AsNoTracking()
                .Where(e => (e.TabelaPrecoId == codTabela) && (e.IdProd == codProduto))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


        }

        [HttpGet("codTabela/nomeProduto")]
        public async Task<IActionResult> GetAllFilteritemProdName([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
          [FromQuery] int? codTabela,
          [FromQuery] string nomeProduto

         )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
               .Where(e => (e.TabelaPrecoId == codTabela) && (e.Produtos.Nome.ToLower().Contains(nomeProduto.ToLower())))
                .OrderBy(e => e.Id).Include(e => e.Produtos)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemTabela
                .AsNoTracking()
                .Where(e => (e.TabelaPrecoId == codTabela) && (e.Produtos.Nome.ToLower().Contains(nomeProduto.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });

         
        }

        [HttpGet("codTabela/grupoId")]
        public async Task<IActionResult> GetAllFilteritemGrupoProd([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
          [FromQuery] int? codTabela,
          [FromQuery] int? grupoId

         )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
              .Where(e => (e.TabelaPrecoId == codTabela) && (e.Produtos.GrupoProdutoId == grupoId))
                .OrderBy(e => e.Id).Include(e => e.Produtos)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemTabela
                .AsNoTracking()
                .Where(e => (e.TabelaPrecoId == codTabela) && (e.Produtos.GrupoProdutoId == grupoId))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


           
        }

        [HttpGet("codTabela")]
        public async Task<IActionResult> GetAllFilteritem([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina,
           [FromQuery] int? codTabela
           
          )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
              .Where(e => (e.TabelaPrecoId == codTabela))
                .OrderBy(e => e.Id).Include(e => e.Produtos)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.ItemTabela
                .AsNoTracking()
               .Where(e => (e.TabelaPrecoId == codTabela))
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


          
        }




        //[HttpGet("{id}")]
        //public async Task<ActionResult<ItemTabela>> Get(int id)
        //{
        //    var item = await _context.ItemTabela.FindAsync(id);
        //    if (item == null)
        //        return BadRequest("Item da tabela de preço não encontrada.");
        //    return Ok(item);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tabela = await _itemTabelaService.GetItemTabelaAsync(id);
                if (tabela == null) return NoContent();

                return Ok(tabela);
            }
            catch (Exception ex)
            {
                return BadRequest("Ítem não encontrado.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<ItemTabela>>> AddItemTabela(ItemTabela item)
        {

            if (_context.ItemTabela.Any(u => u.Id == item.Id))
            {
                return BadRequest("Item da tabela de preço ja existe na base de dados.");
            }
            if (_context.ItemTabela.Any(u => u.Id == item.Id && u.IdProd == item.IdProd))
            {
                return BadRequest("Produto já cadastrado nesta tabela de preço.");
            }
            _context.ItemTabela.Add(item);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Item da tabela de preço criado com sucesso" }));
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, ItemTabelaDto model)
        {
            _itemTabelaService.Update(id, model);
            return Ok(new { message = "Item da tabela de preço atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ItemTabela>>> Delete(int id)
        {
            var item = await _context.ItemTabela.FindAsync(id);
            if (item == null)
                return BadRequest("Item da tabela de preço não encontrado");

            _context.ItemTabela.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Item da tabela de preço excluído com sucesso!");
        }
    }
}
