using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class EtiquetaDto
    {
      
            [Key]
            public int Id { get; set; }

            [StringLength(500, ErrorMessage = "Inserir máximo de 500 caracteres")]
            public string? Titulo { get; set; }

            [StringLength(400, ErrorMessage = "Inserir máximo de 100 caracteres")]
            public string? NomeTxt { get; set; }
            public string? Sql { get; set; }
            public string? Zpl { get; set; }
            public int? Colunas { get; set; }
            public string? PrinterAddress { get; set; }
            public IEnumerable<EtiqParam>? Parametros { get; set; }

            [NotMapped] // Não é necessário persistir no banco de dados
            public IFormFile File { get; set; }
        
    }
}
