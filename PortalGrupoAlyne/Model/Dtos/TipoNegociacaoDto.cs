using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class TipoNegociacaoDto
    {
        [Key]
        public int Id { get; set; }
        public int? Codigo { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Descricao { get; set; }
        public string? Natureza { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
