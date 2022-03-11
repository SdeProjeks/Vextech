using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;
using Vextech_API.DataAccess;
using System.Reflection;
using Vextech_API.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public List<UserModel> Users { get; set; }
        public List<UserMobileModel> phonenumbers { get; set; }
        
        [HttpGet]
        public ActionResult<List<UserModel>> GetAllUsers()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                Users = new();
                string sql = "SELECT users.ID, users.Email, users.Firstname, users.Lastname, users.Password, users.VatID,"+
                    " users.RoleID, roles.Name, users.AddressID, addresses.Address, addresses.PostNumberID, PostNumber, City, post_numbers.CountryID, countries.Country"+
                    " FROM users INNER JOIN roles ON users.RoleID = roles.ID INNER JOIN addresses ON users.AddressID = addresses.ID"+
                    " INNER JOIN post_numbers ON addresses.PostNumberID = post_numbers.ID INNER JOIN countries ON post_numbers.CountryID = countries.ID;";
                var DatabaseResult = SqlDataAccess.LoadData<VUserModel>(sql);
            
                foreach (var user in DatabaseResult)
                {
                    sql = $"SELECT user_phonenumbers.UserID, user_phonenumbers.MobileCategoryID, user_phonenumbers.PhoneNumber, mobile_category.Name FROM user_phonenumbers INNER JOIN mobile_category ON user_phonenumbers.MobileCategoryID = mobile_category.ID WHERE user_phonenumbers.UserID = {user.ID}";
                    var PhonenumberResult = SqlDataAccess.LoadData<VUserMobileModel>(sql);
                    phonenumbers = new();

                    foreach (var databasephonenumbers in PhonenumberResult)
                    {
                        UserMobileModel phonenumber = new()
                        {
                            mobileCategory = new MobileCategoryModel()
                            {
                                ID = databasephonenumbers.MobileCategoryID,
                                Name = databasephonenumbers.Name
                            },
                            PhoneNumber = databasephonenumbers.PhoneNumber
                        };
                        phonenumbers.Add(phonenumber);
                    }

                    UserModel users = new()
                    {
                        ID = user.ID,
                        Role = new()
                        {
                            ID = user.RoleID,
                            Name = user.Name,
                        },
                        Address = new()
                        {
                            ID = user.AddressID,
                            Address = user.Address,
                            PostNumberID = new()
                            {
                                ID = user.PostNumberID,
                                PostNumber = user.PostNumber,
                                City = user.City,
                                CountryID = new()
                                {
                                    ID = user.CountryID,
                                    Country = user.Country
                                },
                            },
                        },
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Password = user.Password,
                        VatID = user.VatID,
                        PhoneNumbers = phonenumbers
                    };
                    Users.Add(users);
                }
                if (Users.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "There is no users");
                }
                return Users;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

        }

        [HttpGet("{id:int}")]
        public ActionResult<List<UserModel>> GetOneUser(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                Users = new();
                string sql = "SELECT users.ID, users.Email, users.Firstname, users.Lastname, users.Password, users.VatID," +
                    " users.RoleID, roles.Name, users.AddressID, addresses.Address, addresses.PostNumberID, PostNumber, City, post_numbers.CountryID, countries.Country" +
                    " FROM users INNER JOIN roles ON users.RoleID = roles.ID INNER JOIN addresses ON users.AddressID = addresses.ID" +
                    $" INNER JOIN post_numbers ON addresses.PostNumberID = post_numbers.ID INNER JOIN countries ON post_numbers.CountryID = countries.ID WHERE users.ID = {id};";
                var DatabaseResult = SqlDataAccess.LoadData<VUserModel>(sql);

                foreach (var user in DatabaseResult)
                {
                    sql = $"SELECT user_phonenumbers.UserID, user_phonenumbers.MobileCategoryID, user_phonenumbers.PhoneNumber, mobile_category.Name FROM user_phonenumbers INNER JOIN mobile_category ON user_phonenumbers.MobileCategoryID = mobile_category.ID WHERE user_phonenumbers.UserID = {user.ID}";
                    var PhonenumberResult = SqlDataAccess.LoadData<VUserMobileModel>(sql);
                    phonenumbers = new();

                    foreach (var databasephonenumbers in PhonenumberResult)
                    {
                        UserMobileModel phonenumber = new()
                        {
                            mobileCategory = new MobileCategoryModel()
                            {
                                ID = databasephonenumbers.MobileCategoryID,
                                Name = databasephonenumbers.Name
                            },
                            PhoneNumber = databasephonenumbers.PhoneNumber
                        };
                        phonenumbers.Add(phonenumber);
                    }

                    UserModel users = new()
                    {
                        ID = user.ID,
                        Role = new()
                        {
                            ID = user.RoleID,
                            Name = user.Name,
                        },
                        Address = new()
                        {
                            ID = user.AddressID,
                            Address = user.Address,
                            PostNumberID = new()
                            {
                                ID = user.PostNumberID,
                                PostNumber = user.PostNumber,
                                City = user.City,
                                CountryID = new()
                                {
                                    ID = user.CountryID,
                                    Country = user.Country
                                },
                            },
                        },
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Password = user.Password,
                        VatID = user.VatID,
                        PhoneNumbers = phonenumbers
                    };
                    Users.Add(users);
                }

                if (Users.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "user not found");
                }
                return Users;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        [HttpPost]
        public ActionResult<UserModel> Userlogin(string email, string password, string session = "null")
        {
            try
            {
                Users = new();
                if (session == "null")
                {
                    string sql;
                    sql = $"SELECT ID, Firstname, Lastname FROM users WHERE Email = '{email}' AND Password = '{password}'";
                    var databaseResult = SqlDataAccess.LoadData<VUserModel>(sql);
                    foreach (var user in databaseResult)
                    {
                        UserModel users = new()
                        {
                            ID = user.ID,
                            Firstname = user.Firstname,
                            Lastname = user.Lastname
                        };
                        Users.Add(users);
                    }
                    if (Users.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status404NotFound, "Password or Email was not found");
                    }

                    Users[0].Session = UserSessionController.UserLoginSessionHandler(Users[0].ID);

                    return Users[0];
                }
                else
                {
                    string sql;
                    sql = $"SELECT ID, Firstname, Lastname FROM users WHERE Email = '{email}' AND Password = '{password}'";
                    var databaseResult = SqlDataAccess.LoadData<VUserModel>(sql);
                    foreach (var User in databaseResult)
                    {
                        UserModel users = new()
                        {
                            ID = User.ID,
                            Firstname = User.Firstname,
                            Lastname = User.Lastname
                        };
                        Users.Add(users);
                    }
                    if (Users.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status404NotFound, "Password or Email was not found");
                    }

                    Users[0].Session = UserSessionController.UserLoginSessionHandler(Users[0].ID, session);

                    return Users[0];
                }
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreateUser(VUserModel userCreate)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");
                //VUserModel data = new()
                //{
                //    AddressID = userCreate.AddressID,
                //    RoleID = userCreate.RoleID,
                //    Email = email,
                //    Firstname = firstname,
                //    Lastname = lastname,
                //    Password = password,
                //    VatID = vatID
                //    };
                
                string sql;
                sql = @"INSERT INTO users (AddressID,RoleID,Email,Firstname,Lastname,Password,VatID) VALUES (@AddressID,@RoleID,@Email,@Firstname,@Lastname,@Password,@VatID);";
                
                var result = SqlDataAccess.SaveData<VUserModel>(sql, userCreate);

                return Ok("Created user succesfully");
            }
            catch (ValidationException ve)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, ve.Message);
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "created user Failed, because of invalid data");
            }
        }

        [HttpPut]
        public ActionResult UpdateUser(UserModel updateUser)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                // Updates the user
                string sql = $"UPDATE users SET RoleID={updateUser.Role.ID}, AddressID={updateUser.Address.ID}, Email='{updateUser.Email}', Firstname='{updateUser.Firstname}', Lastname='{updateUser.Lastname}', Password='{updateUser.Password}', VatID='{updateUser.VatID}' WHERE ID={updateUser.ID};";
                var result = SqlDataAccess.UpdateData(sql);

                if (result == 0)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "user was not updated either because of invalid data or not found");
                }

                // Deleting the users phone
                //sql = $"DELETE FROM user_phonenumbers WHERE UserID = {updateUser.ID}";
                //result = SqlDataAccess.DeleteData(sql);

                //foreach (var phoneNumber in updateUser.PhoneNumbers)
                //{
                //    VUserMobileModel PhoneModel = new()
                //    {
                //        UserID = updateUser.ID,
                //        MobileCategoryID = phoneNumber.mobileCategory.ID,
                //        PhoneNumber = phoneNumber.PhoneNumber
                //    };
                //    sql = @"INSERT INTO user_phonenumbers (UserID,MobileCategoryID,PhoneNumber) VALUES (@UserID, @MobilCategoryID, @PhoneNumber)";
                //    result = SqlDataAccess.SaveData<VUserMobileModel>(sql, PhoneModel);
                //}
                return Ok("Updated the user succesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "User not found or invalid data");
            }

        }
    }
}
