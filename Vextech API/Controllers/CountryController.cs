using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vextech_API.DataAccess;
using Dapper;
using MySqlConnector;
using Dapper.Mapper;

namespace Vextech_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class Country : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<CountrieModel>> GetCountryByID(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT ID, Country FROM countries WHERE ID = {id};";

                using (IDbConnection cnn = new MySqlConnection(SqlDataAccess.GetConnectionString()))
                {
                    var county = cnn.Query<CountrieModel>(sql).ToList();

                    return county;
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}