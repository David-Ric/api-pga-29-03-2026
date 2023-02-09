using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PortalGrupoAlyne.Model
{
    public class SubMenuPermissao
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Nome { get; set; }

        [ForeignKey("MenuPermissao")]
        public int MenuPermissaoId { get; set; }

        public IEnumerable<PaginaPermissao>? PaginaPermissao { get; set; }

        [MaybeNull]
        public int? UsuarioId { get; set; }

        [MaybeNull]
        public int? GrupoUsuarioId { get; set; }
    }
}
