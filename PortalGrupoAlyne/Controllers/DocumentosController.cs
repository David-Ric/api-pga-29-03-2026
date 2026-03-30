using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Data;
using PortalGrupoAlyne.Model;
using System.IO;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/documentos")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] int? usuarioId, [FromQuery] int? parceiroId)
        {
            if ((usuarioId == null && parceiroId == null) || (usuarioId != null && parceiroId != null))
            {
                return BadRequest(new { errors = new { alvo = "Informe usuarioId ou parceiroId (apenas um)." } });
            }

            IQueryable<Documento> query = _context.Documento.AsNoTracking();

            if (usuarioId != null)
            {
                query = query.Where(d => d.UsuarioId == usuarioId);
            }
            else
            {
                query = query.Where(d => d.ParceiroId == parceiroId);
            }

            var docs = await query
                .OrderByDescending(d => d.CriadoEm)
                .Select(d => new
                {
                    d.Id,
                    d.UsuarioId,
                    d.ParceiroId,
                    d.NomeArquivo,
                    d.ContentType,
                    d.TamanhoBytes,
                    d.CriadoEm,
                    d.Descricao,
                    UrlArquivo = $"/api/documentos/{d.Id}/arquivo"
                })
                .ToListAsync();

            return Ok(docs);
        }

        [HttpPost]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> Upload([FromForm] int? usuarioId, [FromForm] int? parceiroId, [FromForm] IFormFile file, [FromForm] string? descricao)
        {
            if ((usuarioId == null && parceiroId == null) || (usuarioId != null && parceiroId != null))
            {
                return BadRequest(new { errors = new { alvo = "Informe usuarioId ou parceiroId (apenas um)." } });
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest(new { errors = new { file = "Nenhum arquivo enviado." } });
            }

            var contentType = (file.ContentType ?? string.Empty).Trim().ToLowerInvariant();
            var ext = Path.GetExtension(file.FileName ?? string.Empty).Trim().ToLowerInvariant();
            var isPdf = contentType == "application/pdf";
            var isImage = contentType.StartsWith("image/");
            var isAllowedByExt = ext == ".pdf" || ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".webp";

            if (!isAllowedByExt && !isPdf && !isImage)
            {
                return BadRequest(new { errors = new { file = "Aceita apenas PDF ou imagens." } });
            }

            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            var doc = new Documento
            {
                UsuarioId = usuarioId,
                ParceiroId = parceiroId,
                NomeArquivo = file.FileName ?? "documento",
                ContentType = file.ContentType ?? "application/octet-stream",
                TamanhoBytes = file.Length,
                Dados = bytes,
                CriadoEm = DateTime.UtcNow,
                Descricao = descricao
            };

            _context.Documento.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                doc.Id,
                doc.UsuarioId,
                doc.ParceiroId,
                doc.NomeArquivo,
                doc.ContentType,
                doc.TamanhoBytes,
                doc.CriadoEm,
                doc.Descricao,
                UrlArquivo = $"/api/documentos/{doc.Id}/arquivo"
            });
        }

        [HttpGet("{id:int}/arquivo")]
        public async Task<IActionResult> Baixar(int id)
        {
            var doc = await _context.Documento.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (doc == null || doc.Dados == null || doc.Dados.Length == 0)
            {
                return NotFound();
            }

            var safeName = string.IsNullOrWhiteSpace(doc.NomeArquivo) ? "documento" : doc.NomeArquivo;
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{safeName.Replace("\"", "")}\"";
            return File(doc.Dados, string.IsNullOrWhiteSpace(doc.ContentType) ? "application/octet-stream" : doc.ContentType);
        }
    }
}
