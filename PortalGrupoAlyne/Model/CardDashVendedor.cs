using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class CardDashVendedor
    {
        [Key]
        public int Id { get; set; }
        public string? CodVendedor { get; set; }
        public decimal? MetaMes { get; set; }
        public decimal? VendaMes { get; set; }
        public decimal? VaorTotalAno { get; set; }
        public decimal? QuantFaturar { get; set; }
        public decimal? ValorFaturar { get; set; }
        public decimal? QuantPedidoOrcamento { get; set; }
        public decimal? ValorPedidoOrcamento { get; set; }
        public decimal? QuantPedido { get; set; }
        public decimal? ValorPedido { get; set; }
        public decimal? ClienteSemVenda { get; set; }


    }
}
