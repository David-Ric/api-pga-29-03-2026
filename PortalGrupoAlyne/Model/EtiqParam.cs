using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class EtiqParam
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "inserir maximo de 100 caracteres")]
        public string? DescParam { get; set; }

        [ForeignKey("Etiqueta")]
        public int? EtiquetaId { get; set; }
    }
}
