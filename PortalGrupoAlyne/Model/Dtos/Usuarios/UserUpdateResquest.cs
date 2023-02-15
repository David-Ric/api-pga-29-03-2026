using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos.Usuarios
{
    public class UserUpdateResquest
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Status { get; set; }
        [ForeignKey("GrupoUsuario")]
        public int? GrupoId { get; set; }

        public GrupoUsuario? GrupoUsuario { get; set; }
        public string? Funcao { get; set; }
        public string? Telefone { get; set; }
        public string? ImagemURL { get; set; }
        public bool? PrimeiroLoginAdm { get; set; }
        public IEnumerable<MenuPermissao>? MenuPermissao { get; set; }
        public IEnumerable<SubMenuPermissao>? SubMenuPermissao { get; set; }
        public IEnumerable<PaginaPermissao>? PaginaPermissao { get; set; }

    }
}
