using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos.Sankhya;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
   // [Authorize]
    [Route("api/Sankhya")]
    [ApiController]
    public class SankhyaEnviarDadosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SankhyaEnviarDadosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("EnviarDados")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> Processar([FromBody] PedidoVendaRequest pedido)
        {
            var response = await SankhyaEnviarDadosService.processarPedido(_configuration, pedido);
            return Ok(new { response });
        }

    }
}