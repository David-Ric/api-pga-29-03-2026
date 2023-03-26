using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class ComunicadoComercial
    {
        [Key]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Texto { get; set; }
        public int? GrupoId { get; set; }
        public DateTime? CriadoEm { get; set; }
    }
}
