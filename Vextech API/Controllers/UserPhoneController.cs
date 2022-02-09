using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserPhoneController : ControllerBase
    {
        public List<UserMobileModel> phoneNumbers { get; set; }

        [HttpPost]
        public ActionResult<List<VUserMobileModel>> CreatePhonenumber(ulong userID, ulong mobilCategoryID, string phoneNumber)
        {
            try
            {
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpGet("{id:int}")]
        public ActionResult<List<UserMobileModel>> GetPhonenumberByUserID(ulong id)
        {
            try
            {
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
                return phoneNumbers;
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

        }
    }
}
