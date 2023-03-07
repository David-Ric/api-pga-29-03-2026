using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class Titulo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? EmpresaId { get; set; }
        [ForeignKey("Parceiro")]
        public int? ParceiroId { get; set; }
        public int? NuUnico { get; set; }
        public int? Parcela { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataVencim { get; set; }
        public decimal? Valor { get; set; }
    }
}
