using Microsoft.AspNetCore.Http;
using Vextech_API.DataAccess;
using Vextech_API.Models;

namespace Vextech_API.Controllers
{
    public class LogsController
    {
        public static void CreateCalledLog(string methodname, string? email)
        {
            string message = $"Method Called:{methodname}";
            if (email != null)
            {
                message += $", {email}";
            }

            LogsModel data = new()
            {
                Message = message,
            };

            string sql = @$"INSERT INTO logs (Message) VALUES (@Message)";
            SqlDataAccess.SaveData<LogsModel>(sql, data);
        }

        public static void CreateExceptionLog(string methodname, string? email, Exception ex)
        {
            string message = $"Exception Caught:{methodname}";
            if (email != null)
            {
                message += $", {email}";
            }

            if (ex != null)
            {
                message += $", {ex.Message}";
            }

            LogsModel data = new()
            {
                Message = message,
            };

            string sql = @$"INSERT INTO logs (Message) VALUES (@Message)";
            SqlDataAccess.SaveData<LogsModel>(sql, data);
        }
    }
}
