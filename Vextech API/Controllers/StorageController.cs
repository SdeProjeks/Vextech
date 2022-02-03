using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<StorageCategoryModel>> GetStorageCategories()
        {
            try
            {
                string sql;
                sql = $"SELECT * FROM storage_category;";
                var result = SqlDataAccess.LoadData<StorageCategoryModel>(sql);

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<StorageCategoryModel>> GetStorageCategories(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT * FROM storage_category WHERE ID = {id};";
                var result = SqlDataAccess.LoadData<StorageCategoryModel>(sql);

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreateStorageCategory(string category)
        {
            try
            {
                StorageCategoryModel data = new StorageCategoryModel()
                {
                    Category = category
                };

                string sql;
                sql = @"INSERT INTO storage_category (Category) VALUES (@Category);";
                var result = SqlDataAccess.SaveData<StorageCategoryModel>(sql, data);

                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        public ActionResult UpdateStorageCategory(int id, string category)
        {
            try
            {
                string sql;
                sql = $"UPDATE storage_category SET Category = \"{category}\" WHERE ID = {id};";
                var result = SqlDataAccess.UpdateData(sql);

                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

    }
}
