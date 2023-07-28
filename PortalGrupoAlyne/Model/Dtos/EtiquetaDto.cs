using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class EtiquetaDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500, ErrorMessage = "inserir maximo de 500 caracteres")]
        public string? Titulo{ get; set; }

        [StringLength(400, ErrorMessage = "inserir maximo de 100 caracteres")]
        public string? NomeTxt { get; set; }
        public string? Sql { get; set; }
        public int? Colunas { get; set; }
        public string? printerAddress { get; set; }
        public IEnumerable<EtiqParam>? Parametros { get; set; }
    }
}
