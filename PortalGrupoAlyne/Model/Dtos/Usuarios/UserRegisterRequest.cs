using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos.Usuarios
{
    public class UserRegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required ]
        public string Password { get; set; } = string.Empty;
       
        public string? Username { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Status { get; set; }
        public int? GrupoId { get; set; }
        public string? Funcao { get; set; }
        public string? Telefone { get; set; }
        public string? ImagemURL { get; set; }

        public int? TempoSessao { get; set; }
    }
}
