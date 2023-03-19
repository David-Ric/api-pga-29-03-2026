using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Model.Dtos;
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
        public async Task<ActionResult<IEnumerable<ComunicadoDto>>> GetComunicados()
        {
            var comunicados = await _context.Comunicado.ToListAsync();

            if (comunicados == null || !comunicados.Any())
            {
                return NotFound();
            }

            comunicados.Reverse(); // Inverte a lista de comunicados

            var comunicadosDTO = new List<ComunicadoDto>();

            foreach (var comunicado in comunicados)
            {
                var imagemBase64 = Convert.ToBase64String(comunicado.Imagem);

                comunicadosDTO.Add(new ComunicadoDto
                {
                    Id = comunicado.Id,
                    Titulo = comunicado.Titulo,
                    ImagemURL = $"/api/comunicado/imagem/{comunicado.Id}",
                    Texto = comunicado.Texto,
                    ImagemBase64 = imagemBase64
                });
            }

            return comunicadosDTO;
        }


        [HttpGet("Lista")]
        public async Task<IActionResult> GetAll([FromServices] DataContext context,
          [FromQuery] int pagina,
           [FromQuery] int totalpagina
          )
        {
            var total = await context.Comunicado.CountAsync();
            var data = await context.Comunicado.AsNoTracking().Skip((pagina - 1) * totalpagina).Take(totalpagina).ToListAsync();

            return Ok(new
            {
                total,
                data = data
            });
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

            return Ok((new { data= comunicado.Id,
                message = "Comunicado criado com sucesso" }));
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
            var img = await context.Comunicado.FirstOrDefaultAsync(e => e.Id == id);

            if (img == null)
            {
                // handle error
                return NotFound();
            }

            if (files == null || files.Count == 0)
            {
                // handle error
                return BadRequest("Nenhum arquivo enviado");
            }

            var file = files.FirstOrDefault();

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

        [HttpPost("comunicado-com-imagem")]
        [AllowAnonymous]
        public async Task<IActionResult> AddComunicadoComImagem([FromServices] DataContext context, [FromForm(Name = "titulo")] string titulo, [FromForm(Name = "texto")] string texto, [FromForm(Name = "imagem")] IFormFile imagem)
        {
            if (imagem == null)
            {
                // handle error
                return BadRequest("Nenhuma imagem enviada");
            }

            var comunicado = new Comunicado { Titulo = titulo, Texto = texto };

            var imageBytes = await ReadFully(imagem.OpenReadStream());
            comunicado.Imagem = imageBytes;

            context.Comunicado.Add(comunicado);
            await context.SaveChangesAsync();

            return Ok(new
            {
                comunicado.Id,
                comunicado.Titulo,
                comunicado.Texto,
                ImagemURL = $"/api/comunicado/imagem/{comunicado.Id}"
            });
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
