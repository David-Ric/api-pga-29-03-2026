using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
   //[Authorize]
    [Route("api/Sankhya")]
    [ApiController]
    public class SankhyaReceberDadosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SankhyaReceberDadosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("ReceberDados")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Processar([FromQuery] string tabela, [FromQuery] int vendedorId)
        {
            var response = await SankhyaReceberDadosService.processar(_configuration, tabela, vendedorId);
            
            return Ok(response);
        }

    }
}