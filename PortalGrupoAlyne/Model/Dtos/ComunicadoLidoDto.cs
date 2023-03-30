using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ComunicadoLidoDto
    {
        [Key]
        public int Id { get; set; }

        public int ComunicadoId { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Titulo { get; set; }
        public DateTime? LidoEm { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioID { get; set; }
    }
}
