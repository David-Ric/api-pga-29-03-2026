using PortalGrupoAlyne.Model.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
   
    public class Produto 
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Nome { get; set; }
        public string? TipoUnid { get; set; }
        public string? TipoUnid2 { get; set; }
        public int? Conv { get; set; }

        [ForeignKey("GrupoProduto")]
        public int GrupoProdutoId { get; set; }
        public GrupoProduto? GrupoProduto  { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
