using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ProdutoConcorrenteDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? CodProduto { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? NomeProduto { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? CodConcorrente { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? NomeConcorrente { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? CodProdutoConcorrente { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? NomeProdutoSimilar { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
