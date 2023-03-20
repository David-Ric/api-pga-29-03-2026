using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenciasUsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PreferenciasUsuarioController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var preferencias = _context.PreferenciasUsuario.ToList();
            var preferenciasDto = _mapper.Map<List<PreferenciasUsuarioDto>>(preferencias);

            return Ok(preferenciasDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var preferencia = _context.PreferenciasUsuario.FirstOrDefault(p => p.Id == id);
            if (preferencia == null)
            {
                return NotFound();
            }

            var preferenciaDto = _mapper.Map<PreferenciasUsuarioDto>(preferencia);

            return Ok(preferenciaDto);
        }

        [HttpPost]
        public IActionResult Create(PreferenciasUsuarioDto preferenciasUsuarioDto)
        {
            var preferencia = _mapper.Map<PreferenciasUsuario>(preferenciasUsuarioDto);
            _context.PreferenciasUsuario.Add(preferencia);
            _context.SaveChanges();

            preferenciasUsuarioDto.Id = preferencia.Id;

            return CreatedAtAction(nameof(GetById), new { id = preferencia.Id }, preferenciasUsuarioDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PreferenciasUsuarioDto preferenciasUsuarioDto)
        {
            var preferencia = _context.PreferenciasUsuario.FirstOrDefault(p => p.Id == id);
            if (preferencia == null)
            {
                return NotFound();
            }

            _mapper.Map(preferenciasUsuarioDto, preferencia);
            _context.PreferenciasUsuario.Update(preferencia);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var preferencia = _context.PreferenciasUsuario.FirstOrDefault(p => p.Id == id);
            if (preferencia == null)
            {
                return NotFound();
            }

            _context.PreferenciasUsuario.Remove(preferencia);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
