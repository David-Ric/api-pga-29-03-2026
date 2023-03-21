using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunicadoLidoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public ComunicadoLidoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {

            var data = await context.ComunicadoLido.AsNoTracking().ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<ComunicadoLidoDto> postLidosDTO)
        {
            try
            {
                var postLidos = _mapper.Map<IEnumerable<ComunicadoLido>>(postLidosDTO);

                foreach (var postLido in postLidos)
                {
                    _context.ComunicadoLido.Add(postLido);
                }

                _context.SaveChanges();

                return Ok("Posts Lidos adicionados com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar Posts Lidos: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var postLido = _context.ComunicadoLido.Find(id);

                if (postLido == null)
                {
                    return NotFound("Post Lido não encontrado!");
                }

                _context.ComunicadoLido.Remove(postLido);
                _context.SaveChanges();

                return Ok("Post Lido removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover Post Lido: {ex.Message}");
            }
        }
    }
}
