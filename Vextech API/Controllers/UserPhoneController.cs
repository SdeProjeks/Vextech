using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserPhoneController : ControllerBase
    {
        public List<UserMobileModel> phoneNumbers { get; set; }
        
        [HttpGet("{id:int}")]
        public ActionResult<List<UserMobileModel>> GetPhonenumberByUserID(ulong id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                phoneNumbers = new();
                string sql;
                sql = $"SELECT user_phonenumbers.PhoneNumber, mobile_category.Name FROM user_phonenumbers INNER JOIN mobile_category ON user_phonenumbers.MobileCategoryID = mobile_category.ID WHERE user_phonenumbers.UserID = {id};";

                var databaseResult = SqlDataAccess.LoadData<VUserMobileModel>(sql);
                foreach (var phonenumber in databaseResult)
                {
                    UserMobileModel phonenumbers = new()
                    {
                        mobileCategory = new()
                        {
                            ID = phonenumber.MobileCategoryID,
                            Name = phonenumber.Name
                        },
                        PhoneNumber = phonenumber.PhoneNumber
                    };
                    phoneNumbers.Add(phonenumbers);
                }
                if (phoneNumbers.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "user has no phone numbers.");
                }
                return phoneNumbers;
                
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreatePhonenumber(ulong userID, ulong mobilCategoryID, string phoneNumber)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                VUserMobileModel data = new VUserMobileModel()
                {
                    UserID = userID,
                    MobileCategoryID = mobilCategoryID,
                    PhoneNumber = phoneNumber
                };

                string sql;
                sql = @"INSERT INTO user_phonenumbers (UserID,MobileCategoryID,PhoneNumber) VALUES (@UserID, @MobileCategoryID, @PhoneNumber)";
                
                var result = SqlDataAccess.SaveData(sql, data);
                return Ok("Added your Phone Number succesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);
                return this.StatusCode(StatusCodes.Status400BadRequest, "Phone number was not created because of invalid data");
            }
        }
    }
}
