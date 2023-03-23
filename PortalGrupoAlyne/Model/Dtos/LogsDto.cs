using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class LogsDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? VersaoApi { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
