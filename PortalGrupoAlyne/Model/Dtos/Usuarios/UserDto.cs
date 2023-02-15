using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos.Usuarios
{
    public class UserDto
    {

        [Key]
        public int Id { get; set; }
        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string Email { get; set; } = string.Empty;

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? NomeCompleto { get; set; }

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
        public bool? PrimeiroLoginAdm { get; set; }
        public IEnumerable<MenuPermissao>? MenuPermissao { get; set; }
        public IEnumerable<SubMenuPermissao>? SubMenuPermissao { get; set; }
        public IEnumerable<PaginaPermissao>? PaginaPermissao { get; set; }
    }
}
