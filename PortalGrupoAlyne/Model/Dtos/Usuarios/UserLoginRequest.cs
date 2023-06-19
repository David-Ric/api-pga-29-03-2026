using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos.Usuarios
{
    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public bool? Conectado { get; set; }

        public int? TempoSessao { get; set; }
    }
}
