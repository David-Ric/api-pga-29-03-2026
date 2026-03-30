using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ItemOrcamentoDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? Filial { get; set; }

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }

        [StringLength(50, ErrorMessage = "inserir no máximo 50 caracteres")]
        public string? PedidoId { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        public float? Quant { get; set; }
        public float? ValUnit { get; set; }
        public float? ValTotal { get; set; }

        [StringLength(4, ErrorMessage = "inserir no máximo 4 caracteres")]
        public string? Baixado { get; set; }

        [StringLength(1, ErrorMessage = "inserir no máximo 1 caractere")]
        public string? Inativo { get; set; }
    }
}
