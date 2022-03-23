using Microsoft.AspNetCore.Mvc;
using Vextech_APP.ViewModels.UserModels;

namespace Vextech_APP.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new();
        IEnumerable<LoginViewModel> login = null;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string apiurl = ConnectionController.getConnectionString();

                // Sets the url
                client.BaseAddress = new Uri(apiurl + "User/");

                string password = Crypto.HashwithSalt(model.Email, model.Password);
                // Contacts the userlogin get endpoint and send email and password with:
                var responseTask = client.GetAsync($"Userlogin?email={model.Email}&password={password}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadFromJsonAsync<UserViewModel>();
                    readtask.Wait();

                    HttpContext.Response.Cookies.Append("Session", readtask.Result.Session);
                    HttpContext.Response.Cookies.Append("firstname", readtask.Result.Firstname);
                    HttpContext.Response.Cookies.Append("lastname", readtask.Result.Lastname);

                    return RedirectToAction("Index","App");
                }
                else
                {
                    login = Enumerable.Empty<LoginViewModel>();
                    ModelState.AddModelError(string.Empty, "Login failed");
                }
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
    }
}
