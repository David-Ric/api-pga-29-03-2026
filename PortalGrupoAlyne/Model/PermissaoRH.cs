using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class PermissaoRH
    {
        [Key]
        public int Id { get; set; }
        public int? GrupoId { get; set; }
    }
}
