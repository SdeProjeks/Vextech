using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        
        [HttpPost]
        public ActionResult<int> CreateProductCategory(string category, int? subcategory)
        {
            try
            {
                ProductCategoryNameModel data = new ProductCategoryNameModel
                {
                    Category = category,
                    Subcategory = subcategory

                };
          
                string sql;
                sql = @"INSERT INTO product_category_names (Category, Subcategory) VALUES (@Category, @Subcategory);";
                var result = SqlDataAccess.SaveData<IProductCategoryNameModel>(sql, data);
                if (result == 0)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest,"Invalid input please change the input and try again.");
                }

                return this.StatusCode(StatusCodes.Status201Created,"Product category has been created.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        public ActionResult<List<ProductCategoryNameModel>> GetMainCategories()
        {
            try
            {
                string sql = "SELECT * FROM product_category_names WHERE product_category_names.Subcategory IS NULL;";
                var result = SqlDataAccess.LoadData<ProductCategoryNameModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "Could not get any maincategories in the database.");
                }

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Database Failure");
            }
        }

        [HttpGet]
        public ActionResult<List<ProductCategoryNameModel>> GetSubCategories()
        {
            try
            {
                string sql = "SELECT * FROM product_category_names WHERE product_category_names.Subcategory IS NOT NULL;";
                var result = SqlDataAccess.LoadData<ProductCategoryNameModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent,"Could not get any subcategories in the database.");
                }

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        public ActionResult UpdateCategory(int id, int subcategoryID, string categoryname)
        {
            try
            {
                string sql = $"UPDATE product_category_names SET Subcategory={subcategoryID}, category={categoryname} WHERE ID={1};";
                var result = SqlDataAccess.UpdateData(sql);

                return Ok("Category was updated.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the category in the database nothing was updated.");
            }
        }
    }
}
