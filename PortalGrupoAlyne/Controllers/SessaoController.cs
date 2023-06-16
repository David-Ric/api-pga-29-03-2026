using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Services;
using System.Globalization;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessaoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        
        public SessaoController(DataContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost("iniciar-sessao")]
        public IActionResult IniciarSessao([FromBody] Sessao sessao)
        {
            try
            {
                // Exclui registros com Nome vazio ou nulo, ou Nome igual ao nome fornecido
                var sessoesAnteriores = _context.Sessao.Where(s => string.IsNullOrEmpty(s.Nome) || s.Nome == sessao.Nome).ToList();
                _context.Sessao.RemoveRange(sessoesAnteriores);

                // Define o status como "S" (online)
                sessao.Online = "S";

                // Adiciona a nova sessão
                _context.Sessao.Add(sessao);
                _context.SaveChanges();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
        }




        [HttpPost("encerrar-sessao")]
        public IActionResult EncerrarSessao([FromBody] Sessao sessao)
        {
            try
            {
                var sessaoExistente = _context.Sessao.FirstOrDefault(s => s.Nome == sessao.Nome && s.Online == "S");
                if (sessaoExistente != null)
                {
                    sessaoExistente.Online = "N";
                    _context.SaveChanges();
                }

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context, [FromQuery] int pagina, [FromQuery] int totalpagina)
        {
            var horaAtual = DateTime.Now; 

            var sessoes = await context.Sessao.ToListAsync();

            foreach (var sessao in sessoes)
            {
                DateTime horaAcesso = sessao.HoraAcesso ?? DateTime.MinValue; 

                if ((horaAtual - horaAcesso).TotalMinutes > 40)
                {
                    sessao.Online = "N"; 
                }
            }

            await context.SaveChangesAsync(); 

            var data = sessoes.Where(s => s.Online == "S" && s.Nome !="admin").Skip((pagina - 1) * totalpagina).Take(totalpagina).ToList();

            return Ok(new
            {
                total = data.Count,
                data = data
            });
        }






        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromServices] DataContext context,
        //   [FromQuery] int pagina,
        //    [FromQuery] int totalpagina
        //   )
        //{
        //    var total = await context.Sessao.CountAsync();
        //    var data = await context.Sessao.Where(s => s.Online == "S").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

        //    return Ok(new
        //    {
        //        total,
        //        data = data
        //    });
        //}
    }
}
