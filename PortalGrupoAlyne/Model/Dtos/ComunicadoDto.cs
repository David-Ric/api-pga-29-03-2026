using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ComunicadoDto
    {
        [Key]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? ImagemURL { get; set; }
        public byte[] Imagem { get; set; } = new byte[0];
        public string? Texto { get; set; }
    }
}
