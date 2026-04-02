using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [Route("api/Sankhya")]
    [ApiController]
    public class SankhyaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SankhyaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("executeQuery")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> executeQuery([FromForm] string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return BadRequest(new { errors = new { sql = "Precisa ser enviado." } });
            var response = await SankhyaService.ExecuteWithLoginLogout(_configuration, () => SankhyaService.executeQuery(_configuration, sql));
            return Ok(response);
        }

        [HttpPost("DadosDashSankhya")]
        public async Task<ActionResult<string>> DadosDashSankhya(string sql)
        {

            var response = await SankhyaService.ExecuteWithLoginLogout(_configuration, () => SankhyaService.executeQuery(_configuration, sql));
            return Ok(response);
        }



    }
}
