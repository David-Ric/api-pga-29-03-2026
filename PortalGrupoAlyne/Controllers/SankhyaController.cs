using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortalGrupoAlyne.Services;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Controllers
{    
    [Route("api/Sankhya")]
    [ApiController]
    public class SankhyaController : ControllerBase
    {
        /*
        private readonly DataContext _context;
        private readonly SankhyaService sankhyaService;

        public SankhyaController(DataContext context) 
        {
            _context = context;
        }
        */

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> login()
        {
            var response = await SankhyaService.login();
            return Ok(response);
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> logout()
        {
            var response = await SankhyaService.logout();            
            return Ok(response);
        }

        [HttpPost("executeQuery")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> executeQuery([FromForm] string sql)
        {
            if (sql == null) return BadRequest(new {errors = new {sql = "Precisa ser enviado."}});
            var response = await SankhyaService.executeQuery(sql);
            return Ok(response);
        }

    }
}