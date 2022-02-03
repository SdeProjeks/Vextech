using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class Address : ControllerBase
    {
        public List<AddressModel> result = new List<AddressModel>();

        [HttpGet]
        public ActionResult<List<AddressModel>> GetAddresses()
        {
            try
            {
                string sql;
                sql = $"SELECT addresses.ID,addresses.Address, addresses.PostNumberID, PostNumber, City, post_numbers.CountryID, countries.Country FROM addresses INNER JOIN post_numbers ON addresses.PostNumberID = post_numbers.ID INNER JOIN countries ON post_numbers.CountryID = countries.ID;";
                var databaseresult = SqlDataAccess.LoadData<VAddressModel>(sql).ToArray<VAddressModel>();

                foreach (var Address in databaseresult)
                {
                    AddressModel remapping = new AddressModel()
                    {
                        ID = Address.ID,
                        Address = Address.Address,
                        PostNumber = new post_numbers()
                        {
                            ID = Address.PostNumberID,
                            PostNumber = Address.PostNumber,
                            City = Address.City,
                            Country = new CountryModel()
                            {
                                ID = Address.CountryID,
                                Country = Address.Country
                            }
                        }
                    };
                    result.Add(remapping);
                };


                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<AddressModel>> GetAddressByID(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT addresses.ID,addresses.Address, addresses.PostNumberID, PostNumber, City, post_numbers.CountryID, countries.Country FROM addresses INNER JOIN post_numbers ON addresses.PostNumberID = post_numbers.ID INNER JOIN countries ON post_numbers.CountryID = countries.ID WHERE Addresses.ID = {id};";
                var databaseresult = SqlDataAccess.LoadData<VAddressModel>(sql).ToArray<VAddressModel>();

                foreach (var Address in databaseresult)
                {
                    AddressModel remapping = new AddressModel()
                    {
                        ID = Address.ID,
                        Address = Address.Address,
                        PostNumber = new post_numbers()
                        {
                            ID = Address.PostNumberID,
                            PostNumber = Address.PostNumber,
                            City = Address.City,
                            Country = new CountryModel()
                            {
                                ID = Address.CountryID,
                                Country = Address.Country
                            }
                        }
                    };
                    result.Add(remapping);
                };


                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
