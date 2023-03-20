using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Data;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissaoRHController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PermissaoRHController(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var permissoes = await _context.PermissaoRH.ToListAsync();
            var permissoesDto = _mapper.Map<IEnumerable<PermissaoRhDto>>(permissoes);
            return Ok(permissoesDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<PermissaoRH>>> AddTabelaPreco(PermissaoRH permissao)
        {

            if (_context.PermissaoRH.Any(u => u.GrupoId == permissao.GrupoId))
            {
                return BadRequest("Já foi condecida permissão a este grupo!");
            }

            _context.PermissaoRH.Add(permissao);
            await _context.SaveChangesAsync();

            return Ok("Permissão concedida com sucesso!" );
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] PermissaoRH[] permissoes)
        //{
        //    // Verificar se já existe permissão para o mesmo grupo
        //    foreach (var permissao in permissoes)
        //    {
        //        var permissaoExistente = await _context.PermissaoRH
        //            .AnyAsync(p => p.GrupoId == permissao.GrupoId);

        //        if (permissaoExistente)
        //        {
        //            return BadRequest("Já foi dada permissão a este grupo!");
        //        }
        //    }

        //    // Salvar as permissões
        //    _context.PermissaoRH.AddRange(permissoes);
        //    await _context.SaveChangesAsync();

        //    return Ok("Permissões concedidas com sucesso!");
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var permissao = await _context.PermissaoRH.FindAsync(id);
            if (permissao == null)
            {
                return NotFound();
            }
            var permissaoDto = _mapper.Map<PermissaoRhDto>(permissao);
            return Ok(permissaoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var permissao = await _context.PermissaoRH.FindAsync(id);
            if (permissao == null)
            {
                return NotFound();
            }
            _context.PermissaoRH.Remove(permissao);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}








