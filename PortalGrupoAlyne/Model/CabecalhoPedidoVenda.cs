using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class CabecalhoPedidoVenda
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? Filial { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? Lote { get; set; }

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }

        [StringLength(18, ErrorMessage = "inserir no máximo 18 caracteres")]
        public string? PalMPV { get; set; }

        [ForeignKey("TipoNegociacao")]
        public int TipoNegociacaoId { get; set; }
        public TipoNegociacao? TipoNegociacao { get; set; }

        [ForeignKey("Parceiro")]
        public int ParceiroId { get; set; }

        public Parceiro? Parceiro { get; set; }
        public DateTime Data { get; set; }

        public decimal? Valor { get; set; }

        public DateTime DataEntrega { get; set; }
       

        [StringLength(256, ErrorMessage = "inserir no máximo 256 caracteres")]
        public string? Observacao { get; set; }

        [StringLength(4, ErrorMessage = "inserir no máximo 4 caracteres")]
        public string? Baixado { get; set; }

        [StringLength(40, ErrorMessage = "inserir no máximo 40 caracteres")]
        public string? pedido { get; set; }

        [StringLength(40, ErrorMessage = "inserir no máximo 40 caracteres")]
        public string? Status { get; set; }
        public IEnumerable<ItemPedidoVenda>? ItemPedidoVenda { get; set; }

    }
}
