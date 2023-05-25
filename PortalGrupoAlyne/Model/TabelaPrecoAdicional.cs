using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class TabelaPrecoAdicional
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? EmpresaId { get; set; }

        [ForeignKey("Produtos")]
        public int IdProd { get; set; }

        [ForeignKey("Parceiro")]
        public int? ParceiroId { get; set; }
        public decimal? Preco { get; set; }
        public Produto? Produtos { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
