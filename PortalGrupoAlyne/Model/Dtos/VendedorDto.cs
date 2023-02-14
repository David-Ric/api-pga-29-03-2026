using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class VendedorDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "inserir maximo de 100 caracteres")]
        public string? Nome { get; set; }
        [StringLength(20, ErrorMessage = "inserir no máximo 20 caracteres")]
        public string? Status { get; set; }
        [StringLength(50, ErrorMessage = "inserir no máximo 50 caracteres")]
        public string? Regiao { get; set; }
        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Email { get; set; }
        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? Tipo { get; set; }
        public bool AtuaCompras { get; set; }
        public DateTime? AtualizadoEm { get; set; }

    }
}
