namespace PortalGrupoAlyne.Infra.Services
{
    public interface IMailService
    {
        Task SendMailAsync(SendMailViewModel model);
       
    }
}
