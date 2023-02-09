using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class TabelaPrecoDto
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Descricao { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public IEnumerable<ItemTabela>? ItemTabela { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
