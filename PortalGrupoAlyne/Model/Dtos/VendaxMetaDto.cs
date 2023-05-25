using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class VendaxMetaDto
    {
        [Key]
        public int Id { get; set; }
        public string? CodVendedor { get; set; }
        public string? Month { get; set; }
        public decimal? Meta { get; set; }
        public decimal? Actual { get; set; }
        public string? Color { get; set; }
    }
}
