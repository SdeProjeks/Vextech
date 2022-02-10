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
        public ActionResult<List<ProductModel>> GetProducts()
        {
            try
            {
                Products = new();
                string sql = "SELECT products.ID, products.Name, products.Active, products.Price, products.Release_date, products.BrandID, product_brand.Brand FROM products INNER JOIN product_brand ON products.BrandID = product_brand.ID WHERE products.Active = 1;";
                var DatabaseResults = SqlDataAccess.LoadData<VProductModel>(sql);

                if (DatabaseResults.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find any products in the database.");
                }

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
                string sql = $"SELECT products.ID, products.Name, products.Description, products.Active, products.Price, products.Release_date, products.BrandID, product_brand.Brand FROM products INNER JOIN product_brand ON products.BrandID = product_brand.ID WHERE products.ID = {id} AND products.Active = 1;";
                var DatabaseResults = SqlDataAccess.LoadData<VProductModel>(sql);

                if (DatabaseResults.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find the product in the database.");
                }

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
                
                return this.StatusCode(StatusCodes.Status404NotFound, "Product you searched for does not exist");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        public ActionResult<List<ProductModel>> AdminGetAllProducts()
        {
            try
            {
                Products = new();
                string sql = "SELECT products.ID, products.Name, products.Active, products.Price, products.Release_date, products.BrandID, product_brand.Brand FROM products INNER JOIN product_brand ON products.BrandID = product_brand.ID;";
                var DatabaseResults = SqlDataAccess.LoadData<VProductModel>(sql);

                if (DatabaseResults.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find any products in the database.");
                }

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
        public ActionResult<ProductModel> AdminGetProduct(int id)
        {
            try
            {
                string sql = $"SELECT products.ID, products.Name, products.Description, products.Active, products.Price, products.Release_date, products.BrandID, product_brand.Brand FROM products INNER JOIN product_brand ON products.BrandID = product_brand.ID WHERE products.ID = {id};";
                var DatabaseResults = SqlDataAccess.LoadData<VProductModel>(sql);

                if (DatabaseResults.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We did not find the product in the database.");
                }

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

                return this.StatusCode(StatusCodes.Status404NotFound, "Product you searched for does not exist");
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

                foreach (var category in categories)
                {
                    VProductCategoriesModel categoryModel = new()
                    {
                        CategoryID = category
                    };
                    sql = @"INSERT INTO product_categories (product_categories.ProductID, product_categories.CategoryID) VALUES ((SELECT MAX(ID) FROM products), @CategoryID);";
                    results = SqlDataAccess.SaveData<VProductCategoriesModel>(sql, categoryModel);
                }

                return this.StatusCode(StatusCodes.Status201Created,"Product was created successfully");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid inputs. Please change your inputs and try again.");
            }
        }

        [HttpPut]
        public ActionResult UpdateProduct(int id, string name, string description, int brandID, decimal price, DateTime? release_date, int active, List<int> categories)
        {
            try
            {
                VProductModel data = new()
                {
                    ID = id,
                    Name = name,
                    Description = description,
                    BrandID = brandID,
                    Price = price,
                    Active = active
                };

                // Update product
                string sql = @"UPDATE products SET Name=@Name, Description=@Description, BrandID=@BrandID, Price=@Price, Release_date=@Release_date, Active=@Active WHERE ID = @ID;";
                var results = SqlDataAccess.SaveData<VProductModel>(sql,data);

                // Delete all connected categories to then reapply them later
                sql = $"DELETE FROM product_categories WHERE ProductID = {id}";
                results = SqlDataAccess.DeleteData(sql);

                // Create all the relation to the products categories.
                foreach (var category in categories)
                {
                    VProductCategoriesModel categoryModel = new()
                    {
                        ProductID = id,
                        CategoryID = category
                    };
                    sql = @"INSERT INTO product_categories (product_categories.ProductID, product_categories.CategoryID) VALUES (@ProductID, @CategoryID);";
                    results = SqlDataAccess.SaveData<VProductCategoriesModel>(sql, categoryModel);
                }

                return Ok("Product was updated successfully");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We did not find the product to update in the database nothing was updated.");
            }
        }


        [HttpPut]
        public ActionResult InactivateProduct(int id)
        {
            try
            {
                VProductModel data = new()
                {
                    ID = id,
                    Active = 0
                };

                // Update product
                string sql = @"UPDATE products SET Active=@Active WHERE ID = @ID;";
                var results = SqlDataAccess.SaveData<VProductModel>(sql, data);

                return Ok("Product was updated successfully made inactive");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the product in the database product was not made inactive.");
            }
        }

        [HttpPut]
        public ActionResult ActivateProduct(int id)
        {
            try
            {
                VProductModel data = new()
                {
                    ID = id,
                    Active = 1
                };

                // Update product
                string sql = @"UPDATE products SET Active=@Active WHERE ID = @ID;";
                var results = SqlDataAccess.SaveData<VProductModel>(sql, data);

                return Ok("Product was updated successfully made active");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the product in the database product was not made active.");
            }
        }
    }
}
