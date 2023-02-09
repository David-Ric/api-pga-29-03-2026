using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class ItemTabela
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TebelaPreco")]
        public int TabelaPrecoId { get; set; }

        [ForeignKey("Produtos")]
        public int IdProd { get; set; }
        public decimal Preco { get; set; }
        public Produto? Produtos { get; set; }
        public DateTime? AtualizadoEm { get; set; }

    }
}
