using Microsoft.AspNetCore.Mvc;

namespace Vextech_APP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
