using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunicadoController : ControllerBase
    {
        private readonly DataContext _context;

        public ComunicadoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Comunicado>> Get()
        {
            return await _context.Comunicado.OrderByDescending(c => c.Id).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Comunicado>> GetById(int id)
        {
            var comunicado = await _context.Comunicado.FindAsync(id);

            if (comunicado == null)
            {
                return NotFound();
            }

            return (comunicado);
        }
        



        [HttpPost]
        public async Task<ActionResult<List<Comunicado>>> AddGrupo(Comunicado comunicado)
        {
           
            _context.Comunicado.Add(comunicado);
            await _context.SaveChangesAsync();

            return Ok((new { message = "Comunicado criado com sucesso" }));
        }

        [HttpGet("imagem/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImagem([FromServices] DataContext context,int id)
        {
            var user = await context.Comunicado.FirstOrDefaultAsync(u => u.Id == id);

            if (user?.Imagem == null)
            {
                return NotFound();
            }

            return File(user.Imagem, "image/jpeg");
        }

        [HttpPost("{id}/imagem")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage([FromServices] DataContext context, int id, IFormFileCollection files)
        {
            var img = await context.Comunicado.FirstOrDefaultAsync(e => e.Id== id);

            if (img == null)
            {
                // handle error
                return NotFound();
            }

            var file = files.FirstOrDefault();

            if (file == null)
            {
                // handle error
                return BadRequest("Nenhum arquivo enviado");
            }

            var imageBytes = await ReadFully(file.OpenReadStream());
            img.Imagem = imageBytes;

            await context.SaveChangesAsync();

            return Ok(new
            {
                img.Id,
                img.Titulo,
                img.Texto,
                ImagemURL = $"/api/comunicado/imagem/{img.Id}"
            });
        }
        private async Task<byte[]> ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Comunicado comunicado, IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            await file.CopyToAsync(ms);
        //            comunicado.Imagem = ms.ToArray();
        //        }
        //    }

        //    await _context.Comunicado.AddAsync(comunicado);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetById), new { id = comunicado.Id }, comunicado);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Comunicado comunicadoAtualizado, IFormFile file)
        {
            var comunicadoExistente = await _context.Comunicado.FindAsync(id);

            if (comunicadoExistente == null)
            {
                return NotFound();
            }

            comunicadoExistente.Titulo = comunicadoAtualizado.Titulo;
            comunicadoExistente.ImagemURL = comunicadoAtualizado.ImagemURL;
            comunicadoExistente.Texto = comunicadoAtualizado.Texto;

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    comunicadoExistente.Imagem = ms.ToArray();
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comunicado = await _context.Comunicado.FindAsync(id);

            if (comunicado == null)
            {
                return NotFound();
            }

            _context.Comunicado.Remove(comunicado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/imagem")]
        public async Task<IActionResult> GetImagem(int id)
        {
            var comunicado = await _context.Comunicado.FindAsync(id);

            if (comunicado == null || comunicado.Imagem == null)
            {
                return NotFound();
            }

            return File(comunicado.Imagem, "image/jpeg");
        }
    }
}
