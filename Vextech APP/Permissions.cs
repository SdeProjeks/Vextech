using System.Text.Json;
using Vextech_APP.Controllers;

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
    }
}
