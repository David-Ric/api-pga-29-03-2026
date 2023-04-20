using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class LigacaoTabela
    {
        [Key]
        public int Id { get; set; }
        public string? CampoLigacao { get; set; }
        public string? TabeaLigada { get; set; }
        public string? CampoExibir { get; set; }

        [ForeignKey("Modulo")]
        public int? ModuloId { get; set; }


    }
}
