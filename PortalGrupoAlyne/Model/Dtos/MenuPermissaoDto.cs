using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class MenuPermissaoDto
    {
        [Key]
        public int Id { get; set; }

        [MaybeNull]
        public int? UsuarioId { get; set; }
        [MaybeNull]
        public int? GrupoUsuarioId { get; set; }
        public int Codigo { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Nome { get; set; }
        public IEnumerable<SubMenuPermissao>? SubMenuPermissao { get; set; }
        public IEnumerable<PaginaPermissao>? PaginaPermissao { get; set; }
    }
}
