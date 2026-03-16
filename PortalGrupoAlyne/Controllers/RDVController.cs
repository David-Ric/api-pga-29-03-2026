using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortalGrupoAlyne.Data;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDVController : ControllerBase
    {
        private readonly DbContext context;
        private readonly RDVService _service;

        public RDVController(RDVService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
            [FromQuery] int pagina,
             [FromQuery] int totalpagina
            )
        {
            var total = await context.Rdvs.CountAsync();
            var data = await context.Rdvs.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

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

            var rdv = await context.Rdvs
                .AsNoTracking()
                .Where(e => (e.NomeCliente.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Rdvs
                .AsNoTracking()
                .Where(e => (e.NomeCliente.ToLower().Contains(filter.ToLower())))
                .CountAsync();

            return Ok(new
            {
                total,
                data = rdv
            });
        }


        [HttpGet("filter/codigo")]
        public async Task<IActionResult> GetAllFilterCodigo([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina,
           [FromQuery] int codigo

           )
        {

            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var rdv = await context.Rdvs
                .AsNoTracking()
                .Where(e => (e.Id == codigo))
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Rdvs
                .AsNoTracking()
                .Where(e => (e.Id == codigo))
                .CountAsync();

            return Ok(new
            {
                total,
                data = rdv
            });
        }




        [HttpGet("{id}")]
        /*
        public async Task<ActionResult<RDV>> Get(int id)
        {
            var rdv = await _service.GetByIdAsync(id);
            if (rdv == null) return NotFound();
            return rdv;
        }
        */
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rdv = await _service.GetByIdAsync(id);
                if (rdv == null) return NoContent();
                return Ok(rdv);
            }
            catch (Exception ex)
            {
                return BadRequest("Rdv não encontrado.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddRdv(RDV rdv)
        {
            await _service.AddAsync(rdv);
            return CreatedAtAction(nameof(GetById), new { id = rdv.Id }, rdv);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RDV rdv)
        {
            if (id != rdv.Id) return BadRequest();
            await _service.UpdateAsync(rdv);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
