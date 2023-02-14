using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class TabelaPrecoParceiro
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }

        [ForeignKey("Parceiro")]
        public int ParceiroId { get; set; }

        [ForeignKey("TabelaPreco")]
        public int TabelaPrecoId { get; set; }
        public TabelaPreco? TabelaPreco { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
