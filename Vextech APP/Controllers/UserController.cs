using Microsoft.AspNetCore.Mvc;
using Vextech_APP.ViewModels.UserModels;

namespace Vextech_APP.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new();
        IEnumerable<LoginViewModel> login = null;
        IEnumerable<UserViewModel> user = null;

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

        [HttpGet("session:string")]
        public IActionResult UserProfile(string session)
        {
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "User/");

            // Contacts the userlogin get endpoint and send email and password with:
            var responseTask = client.GetAsync($"GetUserFromSession?session={session}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<UserViewModel>();
                readtask.Wait();

                return View(readtask.Result);
            }
            else
            {
                user = Enumerable.Empty<UserViewModel>();
                ModelState.AddModelError(string.Empty, "Login failed");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "User/");
            // Get session from cookies
            HttpContext.Request.Cookies.TryGetValue("Session", out string session);
            // Contacts the DeleteUserSession endpoint.
            var responseTask = client.DeleteAsync($"DeleteUserSession?session={session}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                HttpContext.Response.Cookies.Delete("Session");
                HttpContext.Response.Cookies.Delete("firstname");
                HttpContext.Response.Cookies.Delete("lastname");
            }
            return RedirectToAction("index", "app");
        }

        [HttpGet]
        public IActionResult UserDelete()
        {
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "User/");
            // Get session from cookies
            HttpContext.Request.Cookies.TryGetValue("Session", out string session);
            // Contacts the userdelete endpoint.
            var responseTask = client.DeleteAsync($"DeleteUser?session={session}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                HttpContext.Response.Cookies.Delete("Session");
                HttpContext.Response.Cookies.Delete("firstname");
                HttpContext.Response.Cookies.Delete("lastname");

                return RedirectToAction("index","app");
            }

            return RedirectToAction("UserProfile", new { session = session });
        }
    }
}
