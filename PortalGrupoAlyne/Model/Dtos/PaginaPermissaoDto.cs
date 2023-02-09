using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class PaginaPermissaoDto
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Nome { get; set; }

        [ForeignKey("MenuPermissao")]
        public int? MenuPermissaoId { get; set; }
        [MaybeNull]
        public int? SubMenuPermissaoId { get; set; }

        [MaybeNull]
        public int? UsuarioId { get; set; }

        [MaybeNull]
        public int? GrupoUsuarioId { get; set; }
    }
}
