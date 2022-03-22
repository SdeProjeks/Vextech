using Microsoft.Extensions.Configuration;

namespace Vextech_APP.Controllers
{
    public class ConnectionController
    {
        public static string getConnectionString()
        {
            //find and converts appsettings.json to valus/key
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            // get the connectionstring
            return config.GetValue<string>("ConnectionStrings:APIUrl");
        }
    }
}
