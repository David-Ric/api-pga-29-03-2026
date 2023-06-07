using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Services;

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

        //[HttpPost("iniciar-sessao")]
        //public IActionResult IniciarSessao([FromBody] Sessao sessao)
        //{
        //    try
        //    {
        //        var sessoesAnteriores = _context.Sessao.Where(s => s.Nome == sessao.Nome).ToList();
        //        _context.Sessao.RemoveRange(sessoesAnteriores);

        //        sessao.Online = "S";
        //        _context.Sessao.Add(sessao);
        //        _context.SaveChanges();

        //        return Ok();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return StatusCode(500);
        //    }
        //}
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


        //[HttpGet("sessoes-online")]
        //public IActionResult GetSessoesOnline()
        //{
        //    var sessoesOnline = _context.Sessao.Where(s => s.Online == "S")
        //        .ToList();
        //    return Ok(sessoesOnline);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
           [FromQuery] int pagina,
            [FromQuery] int totalpagina
           )
        {
            var total = await context.Sessao.CountAsync();
            var data = await context.Sessao.Where(s => s.Online == "S").AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
        }
    }
}
