using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class GrupoUsuarioDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Nome { get; set; }
        public IEnumerable<MenuPermissao>? MenuPermissao { get; set; }
        public IEnumerable<SubMenuPermissao>? SubMenuPermissao { get; set; }
        public IEnumerable<PaginaPermissao>? PaginaPermissao { get; set; }
       
    }
}
