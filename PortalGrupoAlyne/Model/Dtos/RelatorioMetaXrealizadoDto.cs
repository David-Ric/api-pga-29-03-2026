using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class RelatorioMetaXrealizadoDto
    {
        [Key]
        public int Id { get; set; }
        public string? CodVendedor { get; set; }
        public decimal? Valor01 { get; set; }
        public decimal? Valor02 { get; set; }
    }
}
