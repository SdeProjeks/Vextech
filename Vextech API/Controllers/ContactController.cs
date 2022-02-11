using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.Models;
using Vextech_API.DataAccess;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public ActionResult CreateContact(string name, string email, string message)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, email);

                ContactModel data = new()
                {
                    Name = name,
                    Email = email,
                    Message = message
                };

                string sql = @"INSERT INTO contacts (Name, Email, Message) VALUES (@Name, @Email, @);";
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
        public ActionResult<List<ContactModel>> GetAllContacts()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

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

        [HttpGet("{id:int}")]
        public ActionResult<List<ContactModel>> GetContact(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

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
        public ActionResult DeleteContact(int contactID)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

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
