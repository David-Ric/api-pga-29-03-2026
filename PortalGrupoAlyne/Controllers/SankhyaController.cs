using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortalGrupoAlyne.Services;
using System.Text.RegularExpressions;
using MySqlConnector;
using Newtonsoft.Json;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Controllers
{
    //[Authorize]
    [Route("api/Sankhya")]
    [ApiController]
    public class SankhyaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public SankhyaController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
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

        [HttpPost("DadosDashSankhya")]
        public async Task<ActionResult<string>> DadosDashSankhya(string sql)
        {

            var response = await SankhyaService.executeQuery(_configuration, sql);



            return Ok(response);
        }



    }
}