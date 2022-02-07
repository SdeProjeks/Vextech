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

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
