using Microsoft.AspNetCore.Mvc;
using Vextech_APP.ViewModels.ProductModels;
using Vextech_APP.ViewModels.UserModels;
using Vextech_APP.ViewModels;
using System.Text;
using Newtonsoft.Json;

namespace Vextech_APP.Controllers
{
    
    public class AdminController : Controller
    {
        HttpClient client = new();
        IEnumerable<ContactViewModel> contact = null;
        IEnumerable<UserViewModel> Users = null;
        private object jsonObject;

        public IActionResult index()
        {
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "User/");
            // Contacts an api endpoint inside of User then waits for it to finish:
            var responseTask = client.GetAsync("GetAllUsers");
            responseTask.Wait();

            // Checks the result of the API call and handles it.
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<IList<UserViewModel>>();
                readtask.Wait();

                return View(readtask.Result);
            }
            List<UserViewModel> empty = new();

            return View(empty);
        }

        public IActionResult ContactMesseges()
        {
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "Contact/");
            // Contacts an api endpoint inside of Contact then waits for it to finish:
            var responseTask = client.GetAsync($"GetAllContacts?session={HttpContext.Request.Cookies["Session"]}");
            responseTask.Wait();

            // Checks the result of the API call and handles it.
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<IList<ContactViewModel>>();
                readtask.Wait();

                return View(readtask.Result);
            }

            List<ContactViewModel> empty = new();

            return View(empty);
        }
    }
}
