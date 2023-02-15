namespace PortalGrupoAlyne.Model.Dtos.Sankhya
{
    public class LogoutResponse
    {
        public string serviceName { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string pendingPrinting { get; set; } = string.Empty;
        public string transactionId { get; set; } = string.Empty;
        public string statusMessage { get; set;} = string.Empty;
    }
}