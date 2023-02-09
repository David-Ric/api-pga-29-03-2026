using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class TabelaPrecoClienteDto
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
