using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;
using System.Linq;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PortalGrupoAlyne.Controllers
{
   // [Authorize]
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
        public async Task<IActionResult> GetAll(
    [FromServices] DataContext context,
    [FromQuery] int pagina,
    [FromQuery] int totalpagina
)
        {
            var total = await context.ItemTabela.CountAsync();

            var data = await context.ItemTabela
                .AsNoTracking()
                .Include(i => i.Produtos)
                .OrderBy(p => p.Produtos.Nome)
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina)
                .ToListAsync();

            return Ok(new
            {
                total,
                data
            });
        }



        //     [HttpGet("ItensTotais")]
        //     public async Task<IActionResult> GetAllTotais(
        //[FromServices] DataContext context)
        //     {
        //         var total = await context.ItemTabela.CountAsync();

        //         var data = await context.ItemTabela
        //             .AsNoTracking()
        //             .Include(i => i.Produtos)
        //             .OrderBy(p => p.Produtos.Nome)
        //             .ToListAsync();

        //         return Ok(new
        //         {
        //             total,
        //             data
        //         });
        //     }


            [HttpGet("ItensTotais")]
            public async Task<IActionResult> GetAllTotais(
        [FromServices] DataContext context,
        [FromQuery] int vendedorId)  
            {
                var parceiros = await context.Parceiro
                                              .Where(p => p.VendedorId == vendedorId)
                                              .Select(p => p.id)
                                              .ToListAsync();

                var tabelaPrecoIds = await context.TabelaPrecoParceiro
                                                   .Where(tp => parceiros.Contains(tp.ParceiroId))
                                                   .Select(tp => tp.TabelaPrecoId)
                                                   .ToListAsync();

                var data = await context.ItemTabela
                                    .AsNoTracking()
                                    .Include(i => i.Produtos)
                                    .Where(i => tabelaPrecoIds.Contains(i.TabelaPrecoId))
                                    .OrderBy(p => p.Produtos.Nome)
                                    .ToListAsync();

                return Ok(new
               {
                    total = data.Count,
                    data
                });
            }


       // [HttpGet("ItensTotais")]
    
        //[HttpGet]

        //public async Task<IActionResult> GetAll([FromServices] DataContext context,
        //   [FromQuery] int pagina,
        //    [FromQuery] int totalpagina
        //   )
        //{

        //    var total = await context.ItemTabela.CountAsync();
        //    var data = await context.ItemTabela.AsNoTracking().Include(i => i.Produtos).Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

        //    return Ok(new
        //    {
        //        total,
        //        data = data
        //    });
        //}
        //=======================metodo trazendo as tabelas adicionais ========================================
        [HttpGet("codTabela/codProduto")]
        public async Task<IActionResult> GetAllFilteritemcodProd([FromServices] DataContext context,
            [FromQuery] int pagina,
            [FromQuery] int totalpagina,
            [FromQuery] int? codTabela,
            [FromQuery] string codProduto,
            [FromQuery] int? parceiroId,
            [FromQuery] int? empresaId)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela && e.IdProd.ToString().Contains(codProduto))
                .OrderBy(e => e.Id)
                .Include(e => e.Produtos)
                //.Skip(skip)
                //.Take(take)
                .ToListAsync();

            var tabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId && e.IdProd.ToString().Contains(codProduto))
                 .Include(e => e.Produtos)
                .ToListAsync();

            var totalItemTabela = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela && e.IdProd.ToString().Contains(codProduto))
                .CountAsync();

            var totalTabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId)
                .CountAsync();

            var total = data.Count + tabelaPrecoAdicional.Count;

            var result = new List<object>();
            if (data != null)
                result.AddRange(data);
            if (tabelaPrecoAdicional != null)
                result.AddRange(tabelaPrecoAdicional);

            var paginatedData = result.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                total,
                data = paginatedData
            }); ;
        }

        [HttpGet("codTabela/nomeProduto")]
        public async Task<IActionResult> GetAllFilteritemProdName([FromServices] DataContext context,
    [FromQuery] int pagina,
    [FromQuery] int totalpagina,
    [FromQuery] int? codTabela,
    [FromQuery] string nomeProduto,
    [FromQuery] int? parceiroId,
    [FromQuery] int? empresaId)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela && e.Produtos.Nome.ToLower().Contains(nomeProduto.ToLower()))
                .OrderBy(e => e.Id)
                .Include(e => e.Produtos)
                //.Skip(skip)
                //.Take(take)
                .ToListAsync();

            var tabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId && e.Produtos.Nome.ToLower().Contains(nomeProduto.ToLower()))
                 .Include(e => e.Produtos)
                .ToListAsync();

            var totalItemTabela = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela && e.Produtos.Nome.ToLower().Contains(nomeProduto.ToLower()))
                .CountAsync();

            var totalTabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId)
                .CountAsync();

            var total = data.Count + tabelaPrecoAdicional.Count;

            var result = new List<object>();
            if (data != null)
                result.AddRange(data);
            if (tabelaPrecoAdicional != null)
                result.AddRange(tabelaPrecoAdicional);

            var paginatedData = result.Skip(skip).Take(take).ToList();
            return Ok(new
            {
                total,
                data = paginatedData
            }); ;
        }

        [HttpGet("codTabela/grupoId")]
        public async Task<IActionResult> GetAllFilteritemGrupoProd([FromServices] DataContext context,
    [FromQuery] int pagina,
    [FromQuery] int totalpagina,
    [FromQuery] int? codTabela,
    [FromQuery] int? grupoId,
    [FromQuery] int? parceiroId,
    [FromQuery] int? empresaId)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela && e.Produtos.GrupoProdutoId == grupoId)
                .OrderBy(e => e.Id)
                .Include(e => e.Produtos)
                //.Skip(skip)
                //.Take(take)
                .ToListAsync();

            var tabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId && e.Produtos.GrupoProdutoId == grupoId)
                 .Include(e => e.Produtos)
                .ToListAsync();

            var totalItemTabela = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela && e.Produtos.GrupoProdutoId == grupoId)
                .CountAsync();

            var totalTabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId)
                .CountAsync();

            var total = data.Count + tabelaPrecoAdicional.Count;

            var result = new List<object>();
            if (data != null)
                result.AddRange(data);
            if (tabelaPrecoAdicional != null)
                result.AddRange(tabelaPrecoAdicional);

            var paginatedData = result.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                total,
                data = paginatedData
            }); ;
        }


        [HttpGet("codTabela")]
        public async Task<IActionResult> GetAllFilteritem([FromServices] DataContext context,
      [FromQuery] int pagina,
      [FromQuery] int totalpagina,
      [FromQuery] int? codTabela,
      [FromQuery] int? parceiroId,
      [FromQuery] int? empresaId)
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela)
                .OrderBy(p => p.Produtos.Nome)
                .Include(e => e.Produtos)
                //.Skip(skip)
                //.Take(take)
                .ToListAsync();

            var tabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId)
                 .Include(e => e.Produtos)
                .ToListAsync();

            var totalItemTabela = await context.ItemTabela
                .AsNoTracking()
                .Where(e => e.TabelaPrecoId == codTabela)
                .CountAsync();

            var totalTabelaPrecoAdicional = await context.TabelaPrecoAdicional
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.ParceiroId == parceiroId)
                .CountAsync();

            var total = data.Count + tabelaPrecoAdicional.Count;
            var result = new List<object>();
            if (data != null)
                result.AddRange(data);
            if (tabelaPrecoAdicional != null)
                result.AddRange(tabelaPrecoAdicional);

            var paginatedData = result.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                total,
                data = paginatedData
            }); ;
        }




        //==============itens tabela preço adicional======================================================

        //[HttpGet("tabelaAdicional")]

        //public async Task<IActionResult> GetAllAdicional([FromServices] DataContext context)
         
        //{
        //    var total = await context.TabelaPrecoAdicional.CountAsync();
        //    var data = await context.TabelaPrecoAdicional.AsNoTracking()
        //    .Include(i => i.Produtos)
        //    .ToListAsync();

        //    return Ok(new
        //    {
        //        total,
        //        data = data
        //    });
        //}

        [HttpGet("tabelaAdicional")]
        public async Task<IActionResult> GetAllAdicional([FromServices] DataContext context,
    [FromQuery] int vendedorId)
        {
            var parceiros = await context.Parceiro
                                          .Where(p => p.VendedorId == vendedorId)
                                          .Select(p => (int?)p.id) 
                                          .ToListAsync();

    
            var data = await context.TabelaPrecoAdicional
                                    .Where(tpa => parceiros.Contains(tpa.ParceiroId))
                                    .Include(i => i.Produtos)
                                    .ToListAsync();

            var total = data.Count;

            return Ok(new
            {
                total,
                data = data
            });
        }




        //================================================================================================


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
