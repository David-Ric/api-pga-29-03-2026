using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Migrations;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;


namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddTxtController : ControllerBase
    {
        private readonly string _etiquetasDirectory = "Etiquetas";

       
        [HttpPost]
        public IActionResult UploadTxtFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Nenhum arquivo foi enviado.");
                }

                // Verificar se o diretório "Etiquetas" existe, e, caso não exista, criá-lo.
                if (!Directory.Exists(_etiquetasDirectory))
                {
                    Directory.CreateDirectory(_etiquetasDirectory);
                }

                // Obter o caminho completo do arquivo usando o diretório "Etiquetas" e o nome original do arquivo.
                string filePath = Path.Combine(_etiquetasDirectory, file.FileName);

                // Verificar se o arquivo com o mesmo nome já existe e, se sim, excluí-lo.
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Salvar o novo arquivo no diretório "Etiquetas".
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
                if (!Directory.Exists(_etiquetasDirectory))
                {
                    return NotFound("Diretório 'Etiquetas' não encontrado.");
                }

                string[] fileNames = Directory.GetFiles(_etiquetasDirectory, "*.txt");
                List<object> responseList = new List<object>();

                foreach (string fileName in fileNames)
                {
                    var fileInfo = new { txt = Path.GetFileName(fileName) };
                    responseList.Add(fileInfo);
                }

                return Ok(responseList);
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

                string filePath = Path.Combine(_etiquetasDirectory, nomeArquivo);

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


        [HttpGet("busca-varias/{nomeArquivo}")]
        public IActionResult BuscaVariaveis(string nomeArquivo)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeArquivo))
                {
                    return BadRequest("O nome do arquivo deve ser fornecido como parâmetro.");
                }

                string filePath = Path.Combine(_etiquetasDirectory, nomeArquivo);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Arquivo não encontrado.");
                }

                string fileContent = System.IO.File.ReadAllText(filePath);

                // Aplicar busca de textos entre '$' e '^' usando expressão regular.
                var variables = GetTextBetweenDollarAndCaret(fileContent);

                return Ok(variables);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar as variáveis no arquivo: {ex.Message}");
            }
        }

        private string[] GetTextBetweenDollarAndCaret(string input)
        {
            // Expressão regular para encontrar padrão entre '$' e '^'.
            string pattern = @"\$(.*?)\$";

            // Obter todas as ocorrências que correspondem ao padrão.
            var matches = Regex.Matches(input, pattern);

            // Criar uma lista para armazenar os textos encontrados.
            var variables = new List<string>();

            // Iterar sobre as ocorrências e adicionar os textos à lista de variáveis.
            foreach (Match match in matches)
            {
                variables.Add(match.Groups[1].Value);
            }

            return variables.ToArray();
        }

        [HttpPut("conteudo")]
        public IActionResult PutTxtFileContent([FromQuery] string nomeArquivo, [FromBody] List<KeyValuePair<string, string>> replacements, [FromQuery] int quantPaginas = 1, string printerAddress = "")
        {
            try
            {
                if (string.IsNullOrEmpty(nomeArquivo))
                {
                    return BadRequest("O nome do arquivo deve ser fornecido como parâmetro.");
                }

                string filePath = Path.Combine(_etiquetasDirectory, nomeArquivo);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Arquivo não encontrado.");
                }

                string fileContent = System.IO.File.ReadAllText(filePath);

                // Aplicar as substituições nos valores encontrados entre '$' e '$'.
                fileContent = ReplaceTextBetweenDollarAndDollaruP(fileContent, replacements);

              //  EnviarParaOutraImpressora(fileContent, quantPaginas, printerAddress);

                return Ok(fileContent);
              
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao atualizar o conteúdo do arquivo: {ex.Message}");
            }
        }

        private string ReplaceTextBetweenDollarAndDollaruP(string input, List<KeyValuePair<string, string>> replacements)
        {
            // Expressão regular para encontrar padrão entre '$' e '$'.
            string pattern = @"\$(.*?)\$";

            // Aplicar todas as substituições encontradas na lista.
            foreach (var replacement in replacements)
            {
                input = Regex.Replace(input, pattern, match =>
                {
                    // Obtém o texto capturado entre '$' e '$' (sem os próprios caracteres $).
                    string capturedText = match.Groups[1].Value;

                    // Verifica se o texto capturado corresponde a alguma chave na lista de substituições.
                    if (replacement.Key.Equals(capturedText, StringComparison.OrdinalIgnoreCase))
                    {
                        // Retorna o valor correspondente da substituição.
                        return replacement.Value;
                    }
                    else
                    {
                        // Se não houver substituição correspondente, mantém o texto original.
                        return match.Value;
                    }
                }, RegexOptions.IgnoreCase);
            }

            return input;
        }

        //=============== imprimir ===================================================================================

    private void EnviarParaImpressoraZebra(string zplContent, int numberOfCopies = 1, string? printerAddress="")
        {
        // Converte o conteúdo ZPL em bytes
        byte[] bytes = Encoding.UTF8.GetBytes(zplContent);

        // Cria a conexão TCP/IP com a impressora Zebra
        using (TcpClient client = new TcpClient())
        {
            // Conecta à impressora usando o nome do computador e o nome da impressora
            client.Connect(printerAddress, 9100);

            using (NetworkStream stream = client.GetStream())
            {
                // Envia o código ZPL para a impressora
                for (int i = 0; i < numberOfCopies; i++)
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }



        private void EnviarParaOutraImpressora(string zplContent, int numberOfCopies = 1, string printerName = "")
        {
            // Chama o método da classe RawPrinterHelper para enviar o conteúdo ZPL para a impressora
            RawPrinterHelper.SendStringToPrinter(printerName, zplContent, numberOfCopies);
        }

        public static class RawPrinterHelper
        {
            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool OpenPrinter(string printerName, out IntPtr phPrinter, IntPtr pDefault);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFO di);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

            public static bool SendStringToPrinter(string printerName, string data, int numberOfCopies = 1)
            {
                IntPtr pBytes;
                int dwCount;
                int dwWritten = 0;
                IntPtr hPrinter = IntPtr.Zero;
                DOCINFO di = new DOCINFO
                {
                    pDocName = "My Document",
                    pDataType = "RAW"
                };

                try
                {
                    // Converta o conteúdo ZPL em bytes usando a codificação UTF-8
                    byte[] zplBytes = Encoding.UTF8.GetBytes(data);
                    dwCount = zplBytes.Length;
                    pBytes = Marshal.AllocCoTaskMem(dwCount);
                    Marshal.Copy(zplBytes, 0, pBytes, dwCount);

                    if (OpenPrinter(printerName.Normalize(), out hPrinter, IntPtr.Zero))
                    {
                        if (StartDocPrinter(hPrinter, 1, di))
                        {
                            if (StartPagePrinter(hPrinter))
                            {
                                for (int i = 0; i < numberOfCopies; i++)
                                {
                                    WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                                }
                                EndPagePrinter(hPrinter);
                            }
                            EndDocPrinter(hPrinter);
                        }
                        ClosePrinter(hPrinter);
                    }
                    Marshal.FreeCoTaskMem(pBytes);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public class DOCINFO
            {
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pDocName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pOutputFile;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pDataType;
            }
        }




        //============================================================================================================


        [HttpDelete("excluir")]
        public IActionResult DeleteTxtFile([FromQuery] string nomeArquivo)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeArquivo))
                {
                    return BadRequest("O nome do arquivo deve ser fornecido como parâmetro.");
                }

                string filePath = Path.Combine(_etiquetasDirectory, nomeArquivo);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Arquivo não encontrado.");
                }

                System.IO.File.Delete(filePath);

                return Ok("Arquivo excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao excluir o arquivo: {ex.Message}");
            }
        }
    }
}
