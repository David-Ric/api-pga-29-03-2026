using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PortalGrupoAlyne.Model
{
    public class Pagina
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Nome { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Url { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Icon { get; set; }

        [ForeignKey("Menu")]
        public int? MenuId { get; set; }
        [MaybeNull]
        public int? SubMenuId { get; set; }

    }


}

