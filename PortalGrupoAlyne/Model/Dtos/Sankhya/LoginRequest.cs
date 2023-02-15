using System.Text.Json.Serialization;

namespace PortalGrupoAlyne.Model.Dtos.Sankhya
{
    public class LoginRequest
    {
        public string serviceName { get; set; } = string.Empty;
        public RequestBody requestBody { get; set; } 
    }

    public struct RequestBody 
    {
        public PARAM NOMUSU { get; set; } 
        public PARAM INTERNO { get; set; } 
        public PARAM KEEPCONNECTED { get; set; } 
    }

    public struct PARAM 
    {
        [JsonPropertyName("$")]
        public string ATRIBUTO { get; set; }
    }
}