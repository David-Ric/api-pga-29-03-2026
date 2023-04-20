using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ColunaModuloDto
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public string? TipoInput { get; set; }
        public string? TabInput { get; set; }
        public string? ValueTabInput { get; set; }
        public string? LabelTabInput { get; set; }
        public bool? ChavePrimaria { get; set; }
        public string? Expressao { get; set; }

        [ForeignKey("Modulo")]
        public int? ModuloId { get; set; }
        public IEnumerable<OpcaoCampo>? OpcaoCampo { get; set; }

    }
}
