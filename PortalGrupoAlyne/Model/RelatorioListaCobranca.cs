using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class RelatorioListaCobranca
    {
        [Key]
        public int Id { get; set; }
        public string? CodVendedor { get; set; }
        public string? Valor01 { get; set; }
        public int? Valor02 { get; set; }
        public int? Valor03 { get; set; }
        public string? Valor04 { get; set; }
        public string? Valor05 { get; set; }
        public string? Valor06 { get; set; }
        public string? Valor07 { get; set; }
        public string? Valor08 { get; set; }
        public string? Valor09 { get; set; }
        public string? Valor10 { get; set; }
        public int? Valor11 { get; set; }
        public decimal? Valor12 { get; set; }
        public decimal? Valor13 { get; set; }
        public decimal? Valor14 { get; set; }
        public string? Valor15 { get; set; }
    }
}
