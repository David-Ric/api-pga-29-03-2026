using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PortalGrupoAlyne.Model
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Ordem { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Nome { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Icon { get; set; }
        public IEnumerable<SubMenu>? SubMenu { get; set; }
        public IEnumerable<Pagina>? Pagina { get; set; }

    }
}
