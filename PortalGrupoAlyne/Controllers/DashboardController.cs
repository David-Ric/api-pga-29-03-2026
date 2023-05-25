using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public DashboardController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("grafico")]
        public async Task<ActionResult<IEnumerable<Grafico>>> GetGraficos(string codVendedor)
        {
            var graficos = await _context.Grafico
                .Where(g => g.CodVendedor == codVendedor)
                .ToListAsync();

            return Ok(graficos);
        }


        [HttpGet("vendaXmeta")]
        public async Task<ActionResult<IEnumerable<VendaxMeta>>> GetVendaxMeta(string codVendedor)
        {
            var graficos = await _context.VendaxMeta
                .Where(g => g.CodVendedor == codVendedor)
                .ToListAsync();
            return Ok(graficos);
        }

        [HttpGet("cardDash")]
        public async Task<ActionResult<IEnumerable<CardDashVendedor>>> GetCardDashVendedor(string codVendedor)
        {
            var graficos = await _context.CardDashVendedor
                .Where(g => g.CodVendedor == codVendedor)
                .ToListAsync();
            return Ok(graficos);
        }

        //================ METODOS POST ======================================================================

        [HttpPost("grafico")]
        public async Task<ActionResult<List<Grafico>>> AddGraficos(List<Grafico> graficos)
        {
            string codVendedor = graficos.FirstOrDefault()?.CodVendedor;

            if (string.IsNullOrEmpty(codVendedor))
            {
                return BadRequest("O parâmetro codVendedor é obrigatório.");
            }

            // Excluir os registros existentes com o mesmo codVendedor
            var registrosExistentes = _context.Grafico
                .Where(g => g.CodVendedor == codVendedor);

            _context.Grafico.RemoveRange(registrosExistentes);

            _context.Grafico.AddRange(graficos);
            await _context.SaveChangesAsync();

            return Ok(new { data = graficos, message = "Gráficos adicionados com sucesso." });
        }

        [HttpPost("VendaxMeta")]
        public async Task<ActionResult<List<VendaxMeta>>> AddVendaxMeta(List<VendaxMeta> graficos)
        {
            string codVendedor = graficos.FirstOrDefault()?.CodVendedor;

            if (string.IsNullOrEmpty(codVendedor))
            {
                return BadRequest("O parâmetro codVendedor é obrigatório.");
            }

            // Excluir os registros existentes com o mesmo codVendedor
            var registrosExistentes = _context.VendaxMeta
                .Where(g => g.CodVendedor == codVendedor);

            _context.VendaxMeta.RemoveRange(registrosExistentes);

            _context.VendaxMeta.AddRange(graficos);
            await _context.SaveChangesAsync();

            return Ok(new { data = graficos, message = "VendaxMeta adicionados com sucesso." });
        }
    }
}
