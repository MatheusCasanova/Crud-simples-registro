using Microsoft.AspNetCore.Mvc;

namespace Registro.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
