using System.Text.Json;
using Vextech_APP.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Vextech_APP
{
    public class Permissions
    {

        public static bool PermissionCheck(string permission, string session)
        {
            HttpClient client = new();
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "User/");

            // Contacts the userlogin get endpoint and send email and password with:
            var responseTask = client.GetAsync($"UserHasPermission?permission={permission}&session={session}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<dynamic>();
                readtask.Wait();
                if (JsonSerializer.Serialize(readtask.Result) == "true") return true;
            }
            return false;
        }

        public static bool UsersComment(ulong commentID, string session)
        {
            HttpClient client = new();
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "ProductReview/");
            
            // Contacts the userlogin get endpoint and send email and password with:
            var responseTask = client.GetAsync($"CheckUsersComment?commentID={commentID}&session={session}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<dynamic>();
                readtask.Wait();
                if (JsonSerializer.Serialize(readtask.Result) == "true") return true;
            }

            return false;
        }

        public static bool HasUserCommented(int productID, string session)
        {
            HttpClient client = new();
            string apiurl = ConnectionController.getConnectionString();

            // Sets the url
            client.BaseAddress = new Uri(apiurl + "ProductReview/");

            // Contacts the userlogin get endpoint and send email and password with:
            var responseTask = client.GetAsync($"HasUsersCommented?productID={productID}&session={session}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readtask = result.Content.ReadFromJsonAsync<dynamic>();
                readtask.Wait();
                if (JsonSerializer.Serialize(readtask.Result) == "true") return true;
            }

            return false;
        }
    }
}
