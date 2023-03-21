using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ComunicadoComercialDto
    {
        [Key]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Texto { get; set; }
        public int? GrupoId { get; set; }
    }
}
