using Microsoft.AspNetCore.Mvc;
using Vextech_APP.ViewModels.ProductModels;
using Vextech_APP.ViewModels;
using System.Text;
using Newtonsoft.Json;

namespace Vextech_APP.Controllers
{
    public class AppController : Controller
    {
        HttpClient client = new();
        IEnumerable<ProductViewModel> products = null;
        IEnumerable<ContactViewModel> contact = null;
        private object jsonObject;

        public IActionResult Index()
        {

            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "Product/");
            // Contacts an api endpoint inside of products then waits for it to finish:
            var responseTask = client.GetAsync("GetProducts");
            responseTask.Wait();

            // Checks the result of the API call and handles it.
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<IList<ProductViewModel>>();
                readtask.Wait();

                products = readtask.Result;
            }
            else
            {
                products = Enumerable.Empty<ProductViewModel>();
                ModelState.AddModelError(string.Empty, "an error has occoured. please contact your administrator.");
            }

            return View(products);
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ContactViewModel model = new();
            return View(model);
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string apiurl = ConnectionController.getConnectionString();
                var jsonContent = JsonConvert.SerializeObject(new { name = model.Name, email = model.Email, message = model.Message, session="null" });
                var content = new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json");

                // Sets the uri
                Uri uri = new(apiurl + $"Contact/CreateContact?name={model.Name}&email={model.Email}&message={model.Message}&session=null");
                // Contacts an api endpoint inside of contacts then waits for it to finish:
                var responseTask = client.PostAsync(uri, content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ModelState.Clear();
                    model.Success = "Success";
                    return View(model);
                }
                else
                {
                    contact = Enumerable.Empty<ContactViewModel>();
                    ModelState.AddModelError(string.Empty, "an error has occoured. please try again later.");
                }
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }
    }
}
