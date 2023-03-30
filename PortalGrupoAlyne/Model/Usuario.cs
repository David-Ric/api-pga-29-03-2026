using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [StringLength(300, ErrorMessage = "inserir no máximo 300 caracteres")]
        public string Email { get; set; } = string.Empty;

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Username { get; set; }
        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? NomeCompleto { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenCreated { get; set; }
     
        public DateTime TokenExpires { get; set; }

        [StringLength(200, ErrorMessage = "inserir no máximo 200 caracteres")]
        public string? VerificationToken { get; set; }
        
        public DateTime? VerifiedAt { get; set; }

        [StringLength(200, ErrorMessage = "inserir no máximo 200 caracteres")]
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        [StringLength(20, ErrorMessage = "inserir no máximo 20 caracteres")]
        public string? Status { get; set; }

        [ForeignKey("GrupoUsuario")]
        public int? GrupoId { get; set; }
        public GrupoUsuario? GrupoUsuario { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Funcao { get; set; }
        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Telefone { get; set; }
        [StringLength(200, ErrorMessage = "inserir no máximo 200 caracteres")]
        public string? ImagemURL { get; set; }
        public byte[] Imagem { get; set; } = new byte[0];
        public bool? PrimeiroLoginAdm { get; set; }
        public IEnumerable<MenuPermissao>? MenuPermissao {get; set;}
        public IEnumerable<SubMenuPermissao>? SubMenuPermissao { get; set; }
        public IEnumerable<PaginaPermissao>? PaginaPermissao { get; set; }
        public IEnumerable<PostLido>? PostLido { get; set; }
        public IEnumerable<ComunicadoLido>? ComunicadoLido { get; set; }
        public bool? Conectado { get; set; }
        public List<Message> MensagensRecebidas { get; set; }
        public List<Message> MensagensEnviadas { get; set; }


    }
}
