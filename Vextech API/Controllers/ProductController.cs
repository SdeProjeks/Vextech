using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;
using Vextech_API.Controllers;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public List<ProductModel> Products { get; set; }
        public List<ProductCategoryNameModel> categories { get; set; }

        [HttpGet]
        public ActionResult<List<ProductModel>> GetAllProducts()
        {
            try
            {
                Products = new();
                string sql = "SELECT products.ID, products.Name, products.Active, products.Price, products.Release_date, products.BrandID, product_brand.Brand FROM products INNER JOIN product_brand ON products.BrandID = product_brand.ID;";
                var DatabaseResults = SqlDataAccess.LoadData<VProductModel>(sql);

                foreach (var item in DatabaseResults)
                {
                    sql = $"SELECT product_categories.ProductID, product_categories.CategoryID, product_category_names.Subcategory, product_category_names.Category FROM product_categories INNER JOIN product_category_names ON product_categories.CategoryID = product_category_names.ID WHERE product_categories.ProductID = {item.ID};";
                    var CategoriesResult = SqlDataAccess.LoadData<VProductCategoriesModel>(sql);
                    categories = new();

                    foreach (var Databasecategory in CategoriesResult)
                    {
                        ProductCategoryNameModel category = new()
                        {
                            ID = Databasecategory.CategoryID,
                            Subcategory = Databasecategory.Subcategory,
                            Category = Databasecategory.Category
                        };
                        categories.Add(category);
                    }

                    ProductModel products = new()
                    {
                        ID = item.ID, 
                        Name = item.Name,
                        Price = item.Price,
                        Active = item.Active,
                        Release_date = item.Release_date,
                        Brand = new()
                        {
                            ID = item.BrandID,
                            Brand = item.Brand
                        },
                        Categories = categories
                    };

                    Products.Add(products);
                }

                return Products;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
