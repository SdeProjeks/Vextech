using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;

namespace Vextech_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class Country : ControllerBase
    {
        [HttpGet]
        public ActionResult<CountryModel[]> GetCountryByID(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT ID, Country FROM countries WHERE ID = {id};";
                var result = SqlDataAccess.LoadData<CountryModel>(sql).ToArray();

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}