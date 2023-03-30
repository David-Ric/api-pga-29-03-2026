using Microsoft.AspNetCore.Mvc;

namespace PortalGrupoAlyne.Controllers
{
    public class MensagemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
