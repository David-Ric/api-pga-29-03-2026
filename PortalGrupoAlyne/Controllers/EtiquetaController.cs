using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtiquetaController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public EtiquetaController(DataContext context, IMapper mapper)
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
            var total = await context.Etiqueta.CountAsync();
            var data = await context.Etiqueta.AsNoTracking()
                .Include(i => i.Parametros)
                .Skip((pagina - 1) * totalpagina)
                .Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }

        [HttpGet("filter/titulo")]
        public async Task<IActionResult> GetAllFilterNome([FromServices] DataContext context,
                [FromQuery] int pagina,
                 [FromQuery] int totalpagina,
                 [FromQuery] string filter
                )
        {
            var skip = (pagina - 1) * totalpagina;
            var take = totalpagina;

            var data = await context.Etiqueta
            .Where(e => (e.Titulo.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Include(i => i.Parametros)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await context.Etiqueta
            .AsNoTracking()
                 .Where(e => (e.Titulo.ToLower().Contains(filter.ToLower())))
                .OrderBy(e => e.Id)
                .Include(i => i.Parametros)
                .CountAsync();

            return Ok(new
            {
                total,
                data = data
            });


        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Etiqueta>> Get(int id)
        {
            try
            {
                var etiqueta = await _context.Etiqueta
                    .Include(e => e.Parametros)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (etiqueta == null)
                    return NotFound("Etiqueta não encontrada.");

                return Ok(etiqueta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter a etiqueta: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<Etiqueta>>> AddEtiqueta(Etiqueta etiqueta)
        {
            if (_context.Etiqueta.Any(u => u.Titulo == etiqueta.Titulo))
            {
                return BadRequest("Ja existe uma etiqueta com esse título.");
            }

            _context.Etiqueta.Add(etiqueta);
            await _context.SaveChangesAsync();

            var response = new
            {
                message = "Etiqueta criada com sucesso.",
                id = etiqueta.Id
            };

            return Ok(response);
        }
        



        [HttpPut("{id}")]
        public IActionResult Update(int id, EtiquetaDto etiquetaDto)
        {
            var etiqueta = _context.Etiqueta.FirstOrDefault(p => p.Id == id);
            if (etiqueta == null)
            {
                return NotFound();
            }

            _mapper.Map(etiquetaDto, etiqueta);
            _context.Etiqueta.Update(etiqueta);
            _context.SaveChanges();

            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtiqueta(int id)
        {
            var etiqueta = await _context.Etiqueta.FirstOrDefaultAsync(e => e.Id == id);

            if (etiqueta == null)
            {
                return NotFound("Etiqueta não encontrada.");
            }

            try
            {
                // Antes de excluir a etiqueta, exclua as entidades associadas em EtiqParam
                var etiqParams = await _context.EtiqParam.Where(ep => ep.EtiquetaId == id).ToListAsync();
                _context.EtiqParam.RemoveRange(etiqParams);

                _context.Etiqueta.Remove(etiqueta);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Etiqueta excluída com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir a etiqueta: {ex.Message}");
            }
        }
        //================== paramentros ======================================================================================
        //[HttpPost("parametros")]
        //public async Task<ActionResult> AddEtiqParam(List<EtiqParam> etiquetas)
        //{
        //    try
        //    {
        //        if (etiquetas == null || etiquetas.Count == 0)
        //        {
        //            return BadRequest("A lista de paramtros está vazia ou nula.");
        //        }

        //        foreach (var etiqueta in etiquetas)
        //        {
        //            _context.EtiqParam.Add(etiqueta);
        //        }

        //        await _context.SaveChangesAsync();

        //        var response = new
        //        {
        //            message = "Paramentros criadas com sucesso.",
        //            data = etiquetas
        //        };

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Ocorreu um erro ao salvar os parametros: {ex.Message}");
        //    }
        //}
        [HttpPost("parametros")]
        public async Task<ActionResult> AddEtiqParam(int etiquetaId, List<EtiqParam> etiquetas)
        {
            try
            {
                if (etiquetas == null || etiquetas.Count == 0)
                {
                    return BadRequest("A lista de parametros está vazia ou nula.");
                }

                var existingEtiqParams = await _context.EtiqParam.Where(e => e.EtiquetaId == etiquetaId).ToListAsync();
                _context.EtiqParam.RemoveRange(existingEtiqParams);

                foreach (var etiqueta in etiquetas)
                {
                    etiqueta.EtiquetaId = etiquetaId; 
                    _context.EtiqParam.Add(etiqueta);
                }

                await _context.SaveChangesAsync();

                var response = new
                {
                    message = "Parametros criados com sucesso.",
                    data = etiquetas
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao salvar os parametros: {ex.Message}");
            }
        }



        [HttpPut("parametros")]
        public async Task<ActionResult> UpdateEtiqParam(List<EtiqParam> etiquetas)
        {
            try
            {
                if (etiquetas == null || etiquetas.Count == 0)
                {
                    return BadRequest("A lista de paramentros está vazia ou nula.");
                }

                foreach (var etiqueta in etiquetas)
                {
                    var existingEtiqueta = await _context.EtiqParam.FindAsync(etiqueta.Id);

                    if (existingEtiqueta != null)
                    {
                        // Atualiza a propriedade DescParam da etiqueta existente
                        existingEtiqueta.DescParam = etiqueta.DescParam;

                        // Marca a entidade como modificada para que o contexto saiba que ela precisa ser atualizada
                        _context.Entry(existingEtiqueta).State = EntityState.Modified;
                    }
                    else
                    {
                        // Caso a etiqueta não exista no banco de dados, retorne um erro 404 (Not Found)
                        return NotFound($"Parametro com ID {etiqueta.Id} não encontrado.");
                    }
                }

                // Salva as mudanças no banco de dados
                await _context.SaveChangesAsync();

                var response = new
                {
                    message = "Parametros atualizados com sucesso.",
                    data = etiquetas
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar os parametros: {ex.Message}");
            }
        }


        [HttpDelete("Parametro{id}")]
        public async Task<ActionResult<List<EtiqParam>>> Delete(int id)
        {
            var paramentro = await _context.EtiqParam.FindAsync(id);
            if (paramentro == null)
                return BadRequest("Parametro não encontrado");
            

            _context.EtiqParam.Remove(paramentro);
            await _context.SaveChangesAsync();

            return Ok("Parametro excluído com sucesso!");
        }



    }

}
