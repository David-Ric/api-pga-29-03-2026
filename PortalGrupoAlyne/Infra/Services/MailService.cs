using System.Net.Mail;
using System.Net;

namespace PortalGrupoAlyne.Infra.Services
{
    public class MailService : IMailService
    {
        private string smtpAddress => "smtp.grupoalyne.com.br";
        private int portNumber => 587;
        private string emailFromAddress => "nfe@grupoalyne.com.br";
        private string password => "ciem@010";

        public void AddEmailsToMailMessage(MailMessage mailMessage, string[] emails)
        {
            foreach (var email in emails)
            {
                mailMessage.To.Add(email);
            }
        }

        public async Task SendMailAsync(SendMailViewModel model)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(emailFromAddress, "PGA - Grupo Alyne");
                AddEmailsToMailMessage(mailMessage, model.Emails.ToArray());
                mailMessage.Subject = model.Subject;
                mailMessage.Body = model.Body;
                mailMessage.IsBodyHtml = model.IsHtml;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    await smtp.SendMailAsync(mailMessage);
                }
            }
        }

    }
}
