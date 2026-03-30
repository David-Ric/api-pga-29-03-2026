using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class CabecalhoOrcamentoDto
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

        [StringLength(50, ErrorMessage = "inserir no máximo 50 caracteres")]
        public string? PedidoId { get; set; }

        [ForeignKey("TabelaPreco")]
        public int? TabelaPrecoId { get; set; }
        public TabelaPreco? TabelaPreco { get; set; }

        [ForeignKey("TipoNegociacao")]
        public int TipoNegociacaoId { get; set; }
        public TipoNegociacao? TipoNegociacao { get; set; }

        [StringLength(20, ErrorMessage = "inserir no máximo 20 caracteres")]
        public string? CnpjCpf { get; set; }

        [StringLength(160, ErrorMessage = "inserir no máximo 160 caracteres")]
        public string? NomeParceiro { get; set; }

        [StringLength(200, ErrorMessage = "inserir no máximo 200 caracteres")]
        public string? EndParceiro { get; set; }

        [StringLength(20, ErrorMessage = "inserir no máximo 20 caracteres")]
        public string? NumeroEnd { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? ComplementoEnd { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? Bairro { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? Cidade { get; set; }

        [StringLength(2, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? UF { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? CEP { get; set; }

        public DateTime Data { get; set; }

        public float? Valor { get; set; }

        public DateTime DataEntrega { get; set; }

        [StringLength(256, ErrorMessage = "inserir no máximo 256 caracteres")]
        public string? Observacao { get; set; }

        [StringLength(4, ErrorMessage = "inserir no máximo 4 caracteres")]
        public string? Baixado { get; set; }

        [StringLength(40, ErrorMessage = "inserir no máximo 40 caracteres")]
        public string? Orcamento { get; set; }

        [StringLength(40, ErrorMessage = "inserir no máximo 40 caracteres")]
        public string? Status { get; set; }

        [StringLength(2, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? TipPed { get; set; }

        [StringLength(1, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? Ativo { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? Versao { get; set; }

        public int? Quant_Itens { get; set; }

        [Column(TypeName = "longtext")]
        public string? Log_Envio { get; set; }
    }
}
