using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.Models;
using Vextech_API.DataAccess;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateContact(string name, string email, string message, string session = "null")
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, email);

                // Checks if session has expired
                if (session != "null")
                {
                    var sessionResult = UserSessionController.UserAPISessionHandler(session);

                    switch (sessionResult)
                    {
                        case "Session has expired.":
                            return this.StatusCode(StatusCodes.Status401Unauthorized, "Your session has expired please sign in again.");
                            break;
                    }
                }

                ContactModel data = new()
                {
                    Name = name,
                    Email = email,
                    Message = message
                };

                string sql = @"INSERT INTO contacts (Name, Email, Message) VALUES (@Name, @Email, @Message);";
                var result = SqlDataAccess.SaveData<ContactModel>(sql, data);

                return this.StatusCode(StatusCodes.Status201Created, "Your message has been recieved.");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, email, ex);
                return this.StatusCode(StatusCodes.Status400BadRequest, "Due to bad inputs the message was not created on our side. Change your inputs and try again.");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ContactModel>> GetAllContacts(string session)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                // Checks if session has expired
                var sessionResult = UserSessionController.UserAPISessionHandler(session);
                switch (sessionResult)
                {
                    case "Session has expired.":
                        return this.StatusCode(StatusCodes.Status401Unauthorized, "Your session has expired please sign in again.");
                    break;
                }

                var permissionresult = UserSessionController.SessionPermissionGrant("admin_panel_messages_view", session);
                switch (permissionresult)
                {
                    case "Denied.":
                        return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to view this page");
                        break;
                }

                var sql = "SELECT * FROM contacts";
                var result = SqlDataAccess.LoadData<ContactModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find any support tickets in the database.");
                }

                return result;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}")]
        public ActionResult<List<ContactModel>> GetContact(int id, string session)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                // Checks if session has expired
                var sessionResult = UserSessionController.UserAPISessionHandler(session);
                switch (sessionResult)
                {
                    case "Session has expired.":
                        return this.StatusCode(StatusCodes.Status401Unauthorized, "Your session has expired please sign in again.");
                        break;
                }

                // Check for permission
                var permissionresult = UserSessionController.SessionPermissionGrant("admin_panel_messages_view", session);
                switch (permissionresult)
                {
                    case "Denied.":
                        return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to view this page");
                        break;
                }

                var sql = $"SELECT * FROM contacts WHERE ID={id}";
                var result = SqlDataAccess.LoadData<ContactModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find the support ticket in the database.");
                }

                return result;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete]
        public ActionResult DeleteContact(int contactID, string session)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                // Checks if session has expired
                var sessionResult = UserSessionController.UserAPISessionHandler(session);
                switch (sessionResult)
                {
                    case "Session has expired.":
                        return this.StatusCode(StatusCodes.Status401Unauthorized, "Your session has expired please sign in again.");
                        break;
                }

                // Check for permission
                var permissionresult = UserSessionController.SessionPermissionGrant("admin_panel_messages_delete", session);
                switch (permissionresult)
                {
                    case "Denied.":
                        return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to view this page");
                        break;
                }

                string sql = $"DELETE FROM contacts WHERE ID={contactID};";
                var result = SqlDataAccess.DeleteData(sql);

                return Ok("Contact was successfully deleted.");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the support ticket in the database nothing got deleted.");
            }
        }


    }
}
