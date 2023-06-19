using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model.Dtos.Sankhya;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Configuracao configuracao)
        {
            if (id != configuracao.Id)
            {
               return BadRequest();
            }
            _context.Entry(configuracao).State = EntityState.Modified;
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

        [HttpPut("UpdateSessao")]
        public async Task<IActionResult> UpdateSessao()
        {
            var configuracao = await _context.Configuracao.FindAsync(1);

            if (configuracao == null)
            {
                return NotFound();
            }

            if (configuracao.TempoSessao == null || configuracao.TempoSessao == 0)
            {
                configuracao.TempoSessao = 15;
                await _context.SaveChangesAsync();
                return Ok("Tempo de sessão alterada com sucesso.");
            }
            else
            {
                return Ok("Já existe um tempo de sessão configurado.");
            }
        }


        [HttpGet("integracao{id}")]
        public async Task<ActionResult<Configuracao>> SqlIntegracao(int id, string novoValor)
        {
            var configuracao = await _context.IntegracaoSankhya.FindAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }

            if (configuracao.SqlObterSankhya == novoValor)
            {
                return Ok("SQL já existente");
            }

            configuracao.SqlObterSankhya = novoValor;
            await _context.SaveChangesAsync();

            return Ok(configuracao);
        }


        //[HttpPut("{id}/{tipoConfig}")]
        //public async Task<IActionResult> Update(int id, string tipoConfig, string urlApontamento, Configuracao configuracao)
        //{
        //    Configuracao configToUpdate = null;

        //    if (tipoConfig == "T")
        //    {
        //        configToUpdate = new Configuracao
        //        {
        //            Id = 1,
        //            SankhyaServidor = "http://10.0.0.254:8280/",
        //            SankhyaUsuario = "ADMIN",
        //            SankhyaSenha = "SYNC550V",
        //            UrlApontamento = urlApontamento
        //        };

        //    }
        //    else if (tipoConfig == "P")
        //    {
        //        configToUpdate = new Configuracao
        //        {
        //            Id = 1,
        //            SankhyaServidor = "http://10.0.0.253:8180/",
        //            SankhyaUsuario = "ADMIN",
        //            SankhyaSenha = "SYNC550V",
        //            UrlApontamento = urlApontamento
        //        };

        //    }
        //    else
        //    {
        //        return BadRequest("tipoConfig deve ser 'T' ou 'P'");
        //    }

        //    configToUpdate.Id = id;
        //    _context.Entry(configToUpdate).State = EntityState.Modified;

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
    }
}



