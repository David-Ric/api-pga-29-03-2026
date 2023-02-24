using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class IntegracaoSankhya
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? TabelaPortal { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? ChaveTabelaPortal { get; set; }

        public string? SqlObterSankhya { get; set; }

        public DateTime? AtualizadoEm { get; set; }
    }
}
