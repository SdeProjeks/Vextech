using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;
using Dapper.Mapper;


namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class Address : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<AddressModel>> GetAddresses()
        {
            try
            {
                string sql;
                sql = $"SELECT addresses.ID, addresses.Address, addresses.PostNumberID" +
                    $", post_numbers.ID, post_numbers.PostNumber, post_numbers.City, post_numbers.CountryID" +
                    $", countries.ID, countries.Country " +
                    $"FROM addresses " +
                    $"INNER JOIN post_numbers " +
                    $"ON addresses.PostNumberID = post_numbers.ID " +
                    $"INNER JOIN countries " +
                    $"ON post_numbers.CountryID = countries.ID;";
                using (IDbConnection cnn = new MySqlConnection(SqlDataAccess.GetConnectionString()))
                {
                   
                    var list = cnn.Query<AddressModel,PostNumberModel,CountrieModel,AddressModel>(
                    sql,
                    (AddressModel, PostNumberModel,CountriesModel) =>
                    {
                        AddressModel.PostNumberID = PostNumberModel;
                        AddressModel.PostNumberID.CountryID = CountriesModel;
                        
                        return AddressModel;
                    },
                    splitOn: "PostNumberID, CountryID")
                    .Distinct()
                    .ToList();

                    if (list.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status204NoContent, "We did not find any addresses in the database.");
                    }

                    return list;
                }   
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
                sql = $"SELECT addresses.ID, addresses.Address, addresses.PostNumberID" +
                    $", post_numbers.ID, post_numbers.PostNumber, post_numbers.City, post_numbers.CountryID" +
                    $", countries.ID, countries.Country " +
                    $"FROM addresses " +
                    $"INNER JOIN post_numbers " +
                    $"ON addresses.PostNumberID = post_numbers.ID " +
                    $"INNER JOIN countries " +
                    $"ON post_numbers.CountryID = countries.ID " +
                    $"WHERE Addresses.ID = {id};";
                using (IDbConnection cnn = new MySqlConnection(SqlDataAccess.GetConnectionString()))
                {

                    var list = cnn.Query<AddressModel, PostNumberModel, CountrieModel, AddressModel>(
                    sql,
                    (AddressModel, PostNumberModel, CountriesModel) =>
                    {
                        AddressModel.PostNumberID = PostNumberModel;
                        AddressModel.PostNumberID.CountryID = CountriesModel;

                        return AddressModel;
                    },
                    splitOn: "PostNumberID, CountryID")
                    .Distinct()
                    .ToList();

                    if (list.Count == 0)
                    {
                        return this.StatusCode(StatusCodes.Status204NoContent, "We could not find the address you were looking for in the database.");
                    }

                    return list;
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
