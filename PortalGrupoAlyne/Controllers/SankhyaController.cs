using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortalGrupoAlyne.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/Sankhya")]
    [ApiController]
    public class SankhyaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SankhyaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> login()
        {
            var response = await SankhyaService.login(_configuration);
            return Ok(response);
        }

        [HttpPost("logout")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> logout()
        {
            var response = await SankhyaService.logout(_configuration);
            return Ok(response);
        }

        [HttpPost("executeQuery")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> executeQuery([FromForm] string sql)
        {
            if (sql == null) return BadRequest(new { errors = new { sql = "Precisa ser enviado." } });
            var response = await SankhyaService.executeQuery(_configuration, sql);
            return Ok(response);
        }

    }
}