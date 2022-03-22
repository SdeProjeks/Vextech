using Microsoft.AspNetCore.Mvc;

namespace Vextech_APP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
