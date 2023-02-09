using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model.Dtos.Usuarios
{
    public class UserRegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required
            //, MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres.")
            ]
        public string Password { get; set; } = string.Empty;
        //[Required, Compare("Password")]
        //public string ConfirmPassword { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Status { get; set; }
        public int? GrupoId { get; set; }
        public string? Funcao { get; set; }
        public string? Telefone { get; set; }
        public string? ImagemURL { get; set; }
    }
}
