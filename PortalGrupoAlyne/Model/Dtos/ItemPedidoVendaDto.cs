using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ItemPedidoVendaDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? Filial { get; set; }

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }

        [StringLength(18, ErrorMessage = "inserir no máximo 18 caracteres")]
        public string? PalMPV { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public decimal? Quant { get; set; }
        public decimal? ValUnit { get; set; }

        [StringLength(4, ErrorMessage = "inserir no máximo 4 caracteres")]
        public string? Baixado { get; set; }
    }
}
