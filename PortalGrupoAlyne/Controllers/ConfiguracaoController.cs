using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model.Dtos.Sankhya;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public ConfiguracaoController(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Configuracao>> GetById(int id)
        {
            var configuracao = await _context.Configuracao.FindAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }
            return Ok(configuracao);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, Configuracao configuracao)
        //{
        //    if (id != configuracao.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(configuracao).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_context.Configuracao.Any(e => e.Id == id))
        //        {
        //            return NotFound();
        //        }
        //        throw;
        //    }
        //    return NoContent();
        //}

        [HttpPut("{id}/{tipoConfig}")]
        public async Task<IActionResult> Update(int id, string tipoConfig, Configuracao configuracao)
        {
            Configuracao configToUpdate = null;

            if (tipoConfig == "T")
            {
                configToUpdate = new Configuracao
                {
                    Id = 1,
                    SankhyaServidor = "http://10.0.0.254:8280/",
                    SankhyaUsuario = "ADMIN",
                    SankhyaSenha = "SYNC550V"
                };
                
            }
            else if (tipoConfig == "P")
            {
                configToUpdate = new Configuracao
                {
                    Id = 1,
                    SankhyaServidor = "http://10.0.0.253:8180/",
                    SankhyaUsuario = "ADMIN",
                    SankhyaSenha = "SYNC550V"
                };
               
            }
            else
            {
                return BadRequest("tipoConfig deve ser 'T' ou 'P'");
            }

            configToUpdate.Id = id;
            _context.Entry(configToUpdate).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Configuracao.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }
    }

}
