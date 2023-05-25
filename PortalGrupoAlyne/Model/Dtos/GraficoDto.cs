using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class GraficoDto
    {
        [Key]
        public int Id { get; set; }
        public string? CodVendedor { get; set; }
        public string? Mes { get; set; }
        public decimal? AnoAtual { get; set; }
        public decimal? AnoAnterior { get; set; }

    }
}
