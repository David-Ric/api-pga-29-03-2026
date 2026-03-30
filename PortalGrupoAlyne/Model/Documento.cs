using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [ForeignKey("Parceiro")]
        public int? ParceiroId { get; set; }
        public Parceiro? Parceiro { get; set; }

        [StringLength(255, ErrorMessage = "inserir no máximo 255 caracteres")]
        public string NomeArquivo { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "inserir no máximo 120 caracteres")]
        public string ContentType { get; set; } = string.Empty;

        public long TamanhoBytes { get; set; }

        public byte[] Dados { get; set; } = Array.Empty<byte>();

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        [StringLength(300, ErrorMessage = "inserir no máximo 300 caracteres")]
        public string? Descricao { get; set; }
    }
}
