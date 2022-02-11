using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vextech_API.DataAccess;
using Dapper;
using MySqlConnector;
using Dapper.Mapper;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class Country : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<CountrieModel>> GetAllCountries()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"SELECT ID, Country FROM countries;";

                using (IDbConnection cnn = new MySqlConnection(SqlDataAccess.GetConnectionString()))
                {
                    var country = cnn.Query<CountrieModel>(sql).ToList();

                    if (country.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status204NoContent, "There ar no countries in the database.");
                    }
                    return country;
                }
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<CountrieModel>> GetCountryByID(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"SELECT ID, Country FROM countries WHERE ID = {id};";

                using (IDbConnection cnn = new MySqlConnection(SqlDataAccess.GetConnectionString()))
                {
                    var country = cnn.Query<CountrieModel>(sql).ToList();

                    if (country.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status204NoContent, "We could not find the country in the database.");
                    }
                    return country;
                }
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult Createcountries(List<string> countryNames)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                foreach (var countryname in countryNames)
                {
                    CountrieModel data = new CountrieModel()
                    {
                        Country = countryname,
                    };
                    sql = @"INSERT INTO countries (Country) VALUES (@Country)";
                    var result = SqlDataAccess.SaveData<CountrieModel>(sql, data);

                }
                return Ok("succesfully created all the countries");

            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}