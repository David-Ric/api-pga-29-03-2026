using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/Sankhya")]    
    [ApiController]
    public class SankhyaReceberDadosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public SankhyaReceberDadosController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("ReceberDados")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> Processar()
        {
            var response = await SankhyaReceberDadosService.processar(_configuration, _context);
            return Ok(response);
        }

    }
}