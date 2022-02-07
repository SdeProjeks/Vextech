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

        [HttpGet("{id:int}")]
        public ActionResult<ProductModel> GetProduct(int id)
        {
            try
            {
                string sql = $"SELECT products.ID, products.Name, products.Description, products.Active, products.Price, products.Release_date, products.BrandID, product_brand.Brand FROM products INNER JOIN product_brand ON products.BrandID = product_brand.ID WHERE products.ID = {id};";
                var DatabaseResults = SqlDataAccess.LoadData<VProductModel>(sql);

                if (DatabaseResults.Count > 0)
                {
                    // Gets all the products categories:
                    sql = $"SELECT product_categories.ProductID, product_categories.CategoryID, product_category_names.Subcategory, product_category_names.Category FROM product_categories INNER JOIN product_category_names ON product_categories.CategoryID = product_category_names.ID WHERE product_categories.ProductID = {id};";
                    var CategoriesResult = SqlDataAccess.LoadData<VProductCategoriesModel>(sql);

                    // Creates new list to handle all the products categories:
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

                    // Creates a single product from a model
                    ProductModel products = new()
                    {
                        ID = DatabaseResults[0].ID,
                        Name = DatabaseResults[0].Name,
                        Descrption = DatabaseResults[0].Description,
                        Price = DatabaseResults[0].Price,
                        Active = DatabaseResults[0].Active,
                        Release_date = DatabaseResults[0].Release_date,
                        Brand = new()
                        {
                            ID = DatabaseResults[0].BrandID,
                            Brand = DatabaseResults[0].Brand
                        },
                        Categories = categories
                    };

                    return products;
                }
                
                return this.StatusCode(StatusCodes.Status404NotFound, "Product you searched does not exist");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(string name, string description, int brandID, decimal price, DateTime? release_date, int active, List<int> categories)
        {
            try
            {
                VProductModel data = new()
                {
                    Name = name,
                    Description = description,
                    BrandID = brandID,
                    Price = price,
                    Release_date = release_date,
                    Active = active
                };

                string sql = @"INSERT INTO products (Name, Description, BrandID, Price, Release_date, Active) VALUES (@Name, @Description, @BrandID, @Price, @Release_date, @Active)";
                var results = SqlDataAccess.SaveData<VProductModel>(sql, data);

                if (results == 0)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Some database issue has been encountered your product was not created.");
                }

                return Ok("Product was created successfully");
            }
            catch (Exception)
            {
                throw;
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
