namespace PortalGrupoAlyne.Model.Dtos.Sankhya
{
    public class LoginResponse
    {
        public string serviceName { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string pendingPrinting { get; set; } = string.Empty;
        public string transactionId { get; set; } = string.Empty;
        public ResponseBody responseBody { get; set; }
        public string statusMessage { get; set;} = string.Empty;
    }

    public struct ResponseBody
    {
        public PARAM callID { get; set; } 
        public PARAM jsessionid { get; set; } 
        public PARAM kID { get; set; } 
        public PARAM idusu { get; set; } 
    }
    
}