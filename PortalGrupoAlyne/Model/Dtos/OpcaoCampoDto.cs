using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class OpcaoCampoDto
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public string Opcao { get; set; }
        public string? NomeCampo { get; set; }

        [ForeignKey("ColunaModulo")]
        public int? ColunaModuloId { get; set; }
    }
}
