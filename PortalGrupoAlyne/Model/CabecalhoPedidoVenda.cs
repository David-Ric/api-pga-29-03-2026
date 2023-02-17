using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
    public class CabecalhoPedidoVenda
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2, ErrorMessage = "inserir no máximo 2 caracteres")]
        public string? Filial { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? Lote { get; set; }

        [StringLength(6, ErrorMessage = "inserir no máximo 6 caracteres")]
        public string? Vend { get; set; }

        [StringLength(18, ErrorMessage = "inserir no máximo 18 caracteres")]
        public string? PalMPV { get; set; }

        [StringLength(18, ErrorMessage = "inserir no máximo 18 caracteres")]
        public string? Cond { get; set; }
    }
}
