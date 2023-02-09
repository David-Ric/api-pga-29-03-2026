using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class SubMenuDto
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Ordem { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Nome { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Icon { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public IEnumerable<Pagina>? Pagina { get; set; }
    }
}
