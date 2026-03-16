using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ConfiguracaoDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? SankhyaServidor { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? SankhyaUsuario { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? SankhyaSenha { get; set; }

        public DateTime? AtualizadoEm { get; set; }
        public int? TempoSessao { get; set; }
        public string? Versao { get; set; }
    }
}
