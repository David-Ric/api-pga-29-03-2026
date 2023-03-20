using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class PermissaoRhDto
    {
        [Key]
        public int Id { get; set; }
        public int? GrupoId { get; set; }
    }
}
