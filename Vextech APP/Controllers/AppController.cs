using Microsoft.AspNetCore.Mvc;
using Vextech_APP.ViewModels;

namespace Vextech_APP.Controllers
{
    public class AppController : Controller
    {
        HttpClient client = new();
        IEnumerable<ProductModel> products = null;

        public IActionResult Index()
        {
            // Sets the url
            client.BaseAddress = new Uri("http://localhost:8080/api/Product/");
            // Contacts an api endpoint inside of products then waits for it to finish:
            var responseTask = client.GetAsync("GetProducts");
            responseTask.Wait();

            // Checks the result of the API call and handles it.
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<IList<ProductModel>>();
                readtask.Wait();

                products = readtask.Result;
            }
            else
            {
                products = Enumerable.Empty<ProductModel>();
                ModelState.AddModelError(string.Empty, "an error has occoured. please contact your administrator.");
            }

            return View(products);
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send to database
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
