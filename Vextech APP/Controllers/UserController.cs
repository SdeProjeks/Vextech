using Microsoft.AspNetCore.Mvc;
using Vextech_APP.ViewModels.UserModels;
using Vextech_APP.ViewModels.ProductModels;
using System.Text;
using Newtonsoft.Json;

namespace Vextech_APP.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new();
        IEnumerable<LoginViewModel> login = null;
        IEnumerable<UserViewModel> user = null;

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
        
        [HttpPost]
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

        [HttpGet]
        public IActionResult EditReview(ulong commentID)
        {
            string session = HttpContext.Request.Cookies["Session"];

            // Checks if the user session does have permission to or owns the comment that is about to be added if this fails then it will redirect to the home page.
            if (Vextech_APP.Permissions.PermissionCheck("comments_update_all", session) || Vextech_APP.Permissions.PermissionCheck("comments_update_own", session) && Vextech_APP.Permissions.UsersComment(commentID, session))
            {
                string apiurl = ConnectionController.getConnectionString();

                // Sets the url
                client.BaseAddress = new Uri(apiurl + "ProductReview/");

                // Contacts the userlogin get endpoint and send email and password with:
                var responseTask = client.GetAsync($"GetOneReview/{commentID}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadFromJsonAsync<ProductReviewViewModel>();
                    readtask.Wait();

                    return View(readtask.Result);
                }
                else
                {
                    ProductReviewViewModel empty = new();
                    ModelState.AddModelError(string.Empty, "Could not get the review");
                    return View(empty);
                }
            }

            return RedirectToAction("Index","App");
        }

        [HttpPost]
        public IActionResult EditReview(ProductReviewViewModel model)
        {
            string session = HttpContext.Request.Cookies["Session"];

            // Checks if the user session does have permission to or owns the comment that is about to be added if this fails then it will redirect to the home page.
            if (Vextech_APP.Permissions.PermissionCheck("comments_update_all", session) || Vextech_APP.Permissions.PermissionCheck("comments_update_own", session) && Vextech_APP.Permissions.UsersComment(model.ID, session))
            {
                string apiurl = ConnectionController.getConnectionString();

                // Sets the url
                client.BaseAddress = new Uri(apiurl + "ProductReview/");

                var jsonContent = JsonConvert.SerializeObject(new { ID = model.ID, comment = model.Comment, Rating = model.Rating, session = session });
                var content = new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json");

                // Contacts the userlogin get endpoint and send email and password with:
                var responseTask = client.PutAsync($"UpdateProductReview?ID={model.ID}&comment={model.Comment}&rating={model.Rating}&session={session}", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProductDetails", "App", new { id = model.ProductID });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Could not update the comment.");
                    return View(model);
                }
            }

            return RedirectToAction("Index","App");
        }
    }

    //[HttpGet]
    //public IActionResult DeleteReview(ulong commentID, int productID)
    //{
    //    string session = HttpContext.Request.Cookies["Session"];

    //    // Checks if the user session does have permission to or owns the comment that is about to be added if this fails then it will redirect to the home page.
    //    if (Vextech_APP.Permissions.PermissionCheck("comments_delete_all", session) || Vextech_APP.Permissions.PermissionCheck("comments_delete_own", session) && Vextech_APP.Permissions.UsersComment(commentID, session))
    //    {
    //        string apiurl = ConnectionController.getConnectionString();

    //        // Sets the url
    //        client.BaseAddress = new Uri(apiurl + "ProductReview/");

    //        // Contacts the userlogin get endpoint and send email and password with:
    //        var responseTask = client.GetAsync($"GetOneReview/{commentID}");
    //        responseTask.Wait();

    //        var result = responseTask.Result;
    //        if (result.IsSuccessStatusCode)
    //        {
    //            var readtask = result.Content.ReadFromJsonAsync<ProductReviewViewModel>();
    //            readtask.Wait();

    //            return View(readtask.Result);
    //        }
    //        else
    //        {
    //            ProductReviewViewModel empty = new();
    //            ModelState.AddModelError(string.Empty, "Could not get the review");
    //            return View(empty);
    //        }
    //    }

    //    return RedirectToAction("Index", "App");
    //}
}
