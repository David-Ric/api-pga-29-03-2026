using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class TabelaPrecoCliente
    {
        [Key]
        public int id { get; set; }
        public int CodEmpresa { get; set; }
        [ForeignKey("Parceiros")]
        public int CodParceiro { get; set; }
        public Parceiro? Parceiros { get; set; }

        [ForeignKey("TabelaPreco")]
        public int CodTabelaPreco { get; set; }
        public TabelaPreco? TabelaPreco { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
