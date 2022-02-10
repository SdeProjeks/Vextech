﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.Models;
using Vextech_API.DataAccess;

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

                ContactModel data = new()
                {
                    Name = name,
                    Email = email,
                    Message = message
                };

                string sql = @"INSERT INTO contacts (Name, Email, Message) VALUES (@Name, @Email, @Message);";
                var result = SqlDataAccess.SaveData<ContactModel>(sql, data);

                if (result == 0)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest,"Due to bad inputs the message was not created on our side. Change your inputs and try again.");
                }

                return this.StatusCode(StatusCodes.Status201Created, "Your message has been recieved.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        public ActionResult<List<ContactModel>> GetAllContacts()
        {
            try
            {
                var sql = "SELECT * FROM contacts";
                var result = SqlDataAccess.LoadData<ContactModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find any support tickets in the database.");
                }

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<ContactModel>> GetContact(int id)
        {
            try
            {
                var sql = $"SELECT * FROM contacts WHERE ID={id}";
                var result = SqlDataAccess.LoadData<ContactModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find the support ticket in the database.");
                }

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete]
        public ActionResult DeleteContact(int contactID)
        {
            try
            {

                string sql = $"DELETE FROM contacts WHERE ID={contactID};";
                var result = SqlDataAccess.DeleteData(sql);

                if (result == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the support ticket in the database nothing got deleted.");
                }

                return Ok("Contact was successfully deleted.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


    }
}
