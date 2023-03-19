using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Data;
using System;

namespace PortalGrupoAlyne.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InjecaoSQLController : ControllerBase
    {
        private readonly DataContext _context;

        public InjecaoSQLController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("executar-sql")]
        public IActionResult ExecutarSql([FromQuery] string sql)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(sql);

                return Ok("SQL executado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao executar SQL: {ex.Message}");
            }
        }


    }
}
