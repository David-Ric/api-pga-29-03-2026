using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConexoesController : ControllerBase
    {
        private readonly string _conexoesDirectory = "Conexoes";


        [HttpPost]
        public IActionResult UploadTxtFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Nenhum arquivo foi enviado.");
                }

                // Verificar se o diretório "Conexoes" existe, e, caso não exista, criá-lo.
                if (!Directory.Exists(_conexoesDirectory))
                {
                    Directory.CreateDirectory(_conexoesDirectory);
                }

                // Obter o caminho completo do arquivo usando o diretório "Conexoes" e o nome original do arquivo.
                string filePath = Path.Combine(_conexoesDirectory, file.FileName);

                // Verificar se o arquivo com o mesmo nome já existe e, se sim, excluí-lo.
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Salvar o novo arquivo no diretório "Conexoes".
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return Ok("Arquivo salvo com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao salvar o arquivo: {ex.Message}");
            }
        }


        [HttpGet("lista-nomes")]
        public IActionResult GetTxtFileNames()
        {
            try
            {
                if (!Directory.Exists(_conexoesDirectory))
                {
                    return NotFound("Diretório 'Etiquetas' não encontrado.");
                }

                string[] fileNames = Directory.GetFiles(_conexoesDirectory, "*.xml");
                List<string> names = new List<string>();

                foreach (string fileName in fileNames)
                {
                    names.Add(Path.GetFileName(fileName));
                }

                return Ok(names);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao obter os nomes dos arquivos: {ex.Message}");
            }
        }

        [HttpGet("conteudo")]
        public IActionResult GetTxtFileContent([FromQuery] string nomeArquivo)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeArquivo))
                {
                    return BadRequest("O nome do arquivo deve ser fornecido como parâmetro.");
                }

                string filePath = Path.Combine(_conexoesDirectory, nomeArquivo);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Arquivo não encontrado.");
                }

                string fileContent = System.IO.File.ReadAllText(filePath);

                return Ok(fileContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao obter o conteúdo do arquivo: {ex.Message}");
            }
        }

        

    }
}
