using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class RelatorioClienteQueda
    {
        [Key]
        public int Id { get; set; }
        public string? CodVendedor { get; set; }
        public string? Valor01 { get; set; }
        public int? Valor02 { get; set; }
        public string? Valor03 { get; set; }
        public string? Valor04 { get; set; }
        public decimal? Valor05 { get; set; }
        public decimal? Valor06 { get; set; }
        public decimal? Valor07 { get; set; }
        public decimal? Valor08 { get; set; }
        public decimal? Valor09 { get; set; }
        public decimal? Valor10 { get; set; }
        public decimal? Valor11 { get; set; }
    }
}
