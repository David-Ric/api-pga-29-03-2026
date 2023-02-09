using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class GrupoProduto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? nameGrupo { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
