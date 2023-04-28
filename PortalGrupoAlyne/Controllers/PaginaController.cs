using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{

 // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IPaginaService _paginaService;

        public PaginaController(IMapper mapper, IPaginaService paginaService, DataContext context)
        {
            _paginaService = paginaService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Pagina.CountAsync();
            var data = await context.Pagina.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).OrderBy(e => e.Id).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }


        [HttpGet("default")]
        public async Task<IActionResult> GetAllDefault([FromServices] DataContext context,
    [FromQuery] int pagina,
    [FromQuery] int totalpagina)
        {
            var paginas = context.Pagina.Where(p => p.Url.Contains("/Tela/"));
            var total = await paginas.CountAsync();
            var data = await paginas.Skip((pagina - 1) * totalpagina).Take(totalpagina).OrderBy(e => e.Id).ToListAsync();

            return Ok(new
            {
                total,
                data
            });
        }



        [HttpGet("codigo")]

        public async Task<IActionResult> GetAllFilter([FromServices] DataContext context,
         [FromQuery] int pagina,
          [FromQuery] int totalpagina,
         [FromQuery] int codigo,
         [FromQuery] int idMenu

         )
        {
          //  var total = await context.Pagina.CountAsync();
            var paginas = await context.Pagina.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina)
                                      .Where(e => e.Codigo==codigo && e.MenuId==idMenu)
                         .OrderBy(e => e.Id).ToListAsync();
            var total = paginas.Count();
            return Ok(paginas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pagina>> Get(int id)
        {
           var pagina = await _context.Pagina.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada.");
            return Ok(pagina);
        }

        [HttpDelete("menuCod")]
        public async Task<ActionResult> DeleteAllPagesId(int menuCod)
        {
            try
            {
                var paginas = await _paginaService.GetPaginasPorMenuId(menuCod);
                if (paginas == null) return NoContent();
                _context.Pagina.RemoveRange(paginas);
                await _context.SaveChangesAsync();

                return Ok("Paginas excluídas");
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar paginas. Erro: {ex.Message}");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<List<Pagina>>> AddProduto(Pagina pagina)
        //{

        //    if (_context.Pagina.Any(u => u.Id == pagina.Id))
        //      {
        //        return BadRequest("Página ja existe na base de dados.");
        //    }
        //    if (_context.Pagina.Any(u => u.MenuId == pagina.MenuId && u.Codigo==pagina.Codigo))
        //    {
        //        return BadRequest("Página ja existe na base de dados.");
        //    }
        //    _context.Pagina.Add(pagina);
        //    await _context.SaveChangesAsync();

        //    return Ok((new { message = "Página criada com sucesso" }));
        //}


        [HttpPost]
        public async Task<ActionResult<List<Pagina>>> AddProduto(Pagina pagina)
        {
            // Verifica se a página já existe pelo ID
            if (await _context.Pagina.AnyAsync(p => p.Id == pagina.Id))
            {
                return BadRequest("A página já existe no banco de dados.");
            }

            // Verifica se a página já existe no menu pelo Codigo
            if (await _context.Pagina.AnyAsync(p => p.MenuId == pagina.MenuId && p.Codigo == pagina.Codigo))
            {
                return BadRequest("A página já existe no menu especificado.");
            }

            // Encontra o último valor de Codigo no banco de dados e incrementa em 1
            int lastCodigo = await _context.Pagina
                .OrderByDescending(p => p.Codigo)
                .Select(p => p.Codigo)
                .FirstOrDefaultAsync();
            pagina.Codigo = lastCodigo + 1;

            // Adiciona a nova página ao contexto e salva as alterações
            _context.Pagina.Add(pagina);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Página criada com sucesso." });
        }


        [HttpPut("updateAdmin")]
        public IActionResult UpdateAdmin(string nome, PaginaDto model)
        {
            var pagina = _context.Pagina.FirstOrDefault(p => p.Nome == nome && p.MenuId == 1);

            if (pagina == null)
            {
                return NotFound();
            }

            if (pagina.MenuId == 1)
            {
                pagina.SubMenuId = model.SubMenuId;
                pagina.Icon = model.Icon;
            }

            _context.Pagina.Update(pagina);
            _context.SaveChanges();

            return Ok(new { message = "Página atualizada com sucesso" });
        }

        [HttpPut("updatePagina")]
        public IActionResult UpdatePagina(string nome, PaginaDto model)
        {
            var pagina = _context.Pagina.FirstOrDefault(p => p.Nome == nome && p.MenuId !=1);

            if (pagina == null)
            {
                return NotFound();
            }

            if (pagina.MenuId != 1)
            {
                pagina.MenuId = model.MenuId;
                pagina.Icon = model.Icon;
            }

            _context.Pagina.Update(pagina);
            _context.SaveChanges();

            return Ok(pagina);
        }







        [HttpPut("{id}")]

        public IActionResult Update(int id, PaginaDto model)
        {
            _paginaService.Update(id, model);
            return Ok(new { message = "Página atualizada com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Pagina>>> Delete(int id)
        {
            var pagina = await _context.Pagina.FindAsync(id);
            if (pagina == null)
                return BadRequest("Página não encontrada");

            _context.Pagina.Remove(pagina);
            await _context.SaveChangesAsync();

            return Ok("Página excluída com sucesso!");
        }

    }
}
