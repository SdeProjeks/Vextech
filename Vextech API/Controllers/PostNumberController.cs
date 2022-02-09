using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using Vextech_API.DataAccess;
using Vextech_API.Models.ViewModels;
using Dapper.Mapper;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class Postnumber : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<PostNumberModel>> GetPostNumberByID(int id)
        {
            try
            {
                string sql;
                
                sql = $"SELECT post_numbers.ID,  post_numbers.PostNumber, post_numbers.City, post_numbers.CountryID, countries.ID, countries.Country " +
                    $"FROM post_numbers " +
                    $"INNER JOIN countries " +
                    $"ON post_numbers.CountryID = countries.ID " +
                    $"WHERE post_numbers.ID = {id};";

                using (IDbConnection cnn = new MySqlConnection(SqlDataAccess.GetConnectionString()))
                {
                    
                    var postNumbers = cnn.Query<PostNumberModel, CountrieModel, PostNumberModel>(sql, (PostNumberModel, CountriesModel) =>
                    {
                        PostNumberModel.CountryID = CountriesModel;

                        return PostNumberModel;
                    }, splitOn: "CountryID").Distinct().ToList();


                    return Ok(postNumbers);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
                       
    }
}
