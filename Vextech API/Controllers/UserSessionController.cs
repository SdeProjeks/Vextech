using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.Models;
using Vextech_API.DataAccess;
using System.Reflection;
using System.Net.Http;
using Dapper;

namespace Vextech_API.Controllers
{
    public class UserSessionController : ControllerBase
    {
        public static string UserLoginSessionHandler(ulong userid, string oldsession = "null")
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                // Checks if an oldsession has been parsed
                if (oldsession != "null")
                {
                    // Goes through the UserAPISessionHandler where it either updates the expire time or deletes the session 
                    return UserAPISessionHandler(oldsession);
                }
                else
                {
                    // Creates session if it does not already exist
                    string session = createSession(userid);

                    return session;
                }
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return "The database encountered an issue please try again later.";
            }
        }

        public static string UserAPISessionHandler(string oldsession)
        {
            // Checks if the session exists
            var oldsessionresult = SessionExist(oldsession);
            DateTime now = DateTime.UtcNow;

            // Checks if the expire date is lower than current datetime if so just update the expiretime with same sesion.
            if (oldsessionresult.Expires >= now)
            {
                updateSession(oldsession);

                return "Session has been updated.";
            }
            else // If the session date has expired logout user
            {
                deleteOldSession(oldsession);

                return "Session has expired.";
            }
        }

        public static string SessionPermissionGrant(string permission, string oldsession)
        {
            string sql = $"SELECT permissions.Name FROM user_sessions INNER JOIN users ON user_sessions.UserID = users.ID INNER JOIN roles ON users.RoleID = roles.ID INNER JOIN role_has_permission ON role_has_permission.RoleID = roles.ID INNER JOIN permissions ON role_has_permission.PermissionID = permissions.ID WHERE user_sessions.ID='{oldsession}' AND permissions.Name='{permission}';";

            var result = SqlDataAccess.LoadData<PermissionModel>(sql);

            if (result.Count == 0)
            {
                return "Denied";
            }
            else if (result.Count == 1 && result[0].Name == permission)
            {
                return "Granted";
            }
            else
            {
                throw new ArgumentException("Bad request more than one result was gotten sql injection?","Param");
            }
        }

        public static UserSessionModel SessionExist(string session)
        {
            string sql = $"SELECT * FROM user_sessions WHERE ID='{session}';";

            var result = SqlDataAccess.LoadData<UserSessionModel>(sql);

            if (result.Count == 0)
            {
                throw new ArgumentException("Session was not found","Session");
            }

            return result[0];
        }

        public static string createSession(ulong userid)
        {
            deleteUserSession(userid);

            Random random = new Random();
            int length = 100;
            var session = "";
            for (var i = 0; i < length; i++)
            {
                session += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }

            UserSessionModel data = new()
            {
                ID = session,
                UserID = userid,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            string sql = @"INSERT INTO User_Sessions (ID, UserID, Expires) VALUES (@ID, @UserID, @Expires)";

            SqlDataAccess.SaveData<UserSessionModel>(sql, data);

            return session;
        }

        public static void deleteOldSession(string oldsession)
        {
            string sqlstring = $"DELETE FROM user_sessions WHERE ID='{oldsession}';";

            var sessionUpdateResult = SqlDataAccess.DeleteData(sqlstring);
        }

        public static void deleteUserSession(ulong userid)
        {
            string sqlstring = $"DELETE FROM user_sessions WHERE UserID='{userid}';";

            var sessionUpdateResult = SqlDataAccess.DeleteData(sqlstring);
        }

        public static void updateSession(string oldsession)
        {
            string expire = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");
            string sqlstring = $"update user_sessions SET Expires='{expire}' WHERE ID='{oldsession}';";

            var sessionUpdateResult = SqlDataAccess.UpdateData(sqlstring);
        }
    }
}
