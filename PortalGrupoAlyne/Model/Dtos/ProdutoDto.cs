using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ProdutoDto
    {
        [Key]
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? GrupoId { get; set; }
        public string? NomeGrupo { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
