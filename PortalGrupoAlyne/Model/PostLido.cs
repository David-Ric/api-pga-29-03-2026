using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class PostLido
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Titulo { get; set; }
        public DateTime? LidoEm { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioID { get; set; }
    }
}
