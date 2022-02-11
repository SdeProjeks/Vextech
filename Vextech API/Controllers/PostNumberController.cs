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

                    if (postNumbers.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status204NoContent,"We could not find the postnumber in the database.");
                    }

                    return postNumbers;
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpPost]
        public ActionResult CreatePostNumbers(List<VPostNumberModel> postNumbers)
        {
            try
            {
                string sql;
                foreach (var postnumber in postNumbers)
                {
                    VPostNumberModel data = new()
                    {
                        CountryID = postnumber.CountryID,
                        City = postnumber.City,
                        PostNumber = postnumber.PostNumber
                    };
                    sql = @"INSERT INTO post_numbers (CountryID, PostNumber, City) VALUES (@CountryID, @PostNumber, @City)";
                    var result = SqlDataAccess.SaveData<VPostNumberModel>(sql, data);

                }
                return Ok("succesfully created all the Post Numbers");

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
                       
    }
}
