using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class Postnumber : ControllerBase
    {
        public List<post_numbers> result = new List<post_numbers>();


        [HttpGet]
        public ActionResult<List<post_numbers>> GetPostNumberByID(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT post_numbers.ID, countries.ID, countries.Country, PostNumber, City FROM post_numbers INNER JOIN countries ON post_numbers.CountryID = countries.ID WHERE post_numbers.ID = {id};";
                var databaseresult = SqlDataAccess.LoadData<VPostNumberModel>(sql).ToArray<VPostNumberModel>();

                foreach (var postnumber in databaseresult)
                {
                    post_numbers remapping = new post_numbers()
                    {
                        ID = postnumber.ID,
                        Country = new CountryModel() { ID = postnumber.CountryID, Country = postnumber.Country },
                        PostNumber = postnumber.PostNumber,
                        City = postnumber.City,
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
