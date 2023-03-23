using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalGrupoAlyne.Infra.Services;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/Email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public EmailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public IActionResult SendMail([FromBody] SendMailViewModel sendMailViewModel)
        {
            //_mailService.SendMail(sendMailViewModel.Emails, sendMailViewModel.Subject, sendMailViewModel.Body,
            //    sendMailViewModel.IsHtml);

            return Ok("portalgrupoalyne");
        }
    }
}
