using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostLidosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostLidosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {
         
            var data = await context.PostLido.AsNoTracking().ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<PostLidoDto> postLidosDTO)
        {
            try
            {
                var postLidos = _mapper.Map<IEnumerable<PostLido>>(postLidosDTO);

                foreach (var postLido in postLidos)
                {
                    _context.PostLido.Add(postLido);
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
                var postLido = _context.PostLido.Find(id);

                if (postLido == null)
                {
                    return NotFound("Post Lido não encontrado!");
                }

                _context.PostLido.Remove(postLido);
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
