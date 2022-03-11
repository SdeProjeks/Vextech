using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;
using Vextech_API.DataAccess;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        public List<ProductReviewModel> productReviews {get; set;}

        [HttpGet]
        public ActionResult<List<ProductReviewModel>> GetAllReviews()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                productReviews = new();
                string sql;
                sql = "SELECT products.Name, products.Price, users.Firstname, users.Lastname, product_reviews.ID, product_reviews.`Comment`, product_reviews.Rating, product_reviews.Date FROM product_reviews INNER JOIN users ON product_reviews.UserID = users.ID INNER JOIN products ON product_reviews.ProductID = products.ID;";

                var result = SqlDataAccess.LoadData<VProductReviewModel>(sql);
                foreach (var Review in result)
                {
                    ProductReviewModel remaping = new()
                    {
                        ID = Review.ID,
                        Product = new()
                        {
                            Name = Review.Name,
                            Price = Review.Price
                        },
                        User = new()
                        {
                            Firstname = Review.Firstname,
                            Lastname = Review.Lastname
                        },
                        Comment = Review.comment,
                        Rating = Review.Rating
                    };
                    productReviews.Add(remaping);
                }
                if (productReviews.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "There are no product reviews");
                }
                return productReviews;
                
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpGet("{id:int}")]
        public ActionResult<List<ProductReviewModel>> GetOneReview(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                productReviews = new();
                string sql;
                sql = $"SELECT product_reviews.ID, products.Name, products.Price, products.Description, product_brand.Brand, users.Firstname, users.Lastname, product_reviews.`Comment`, product_reviews.Rating, product_reviews.Date FROM product_reviews INNER JOIN users ON product_reviews.UserID = users.ID INNER JOIN products ON product_reviews.ProductID = products.ID INNER JOIN product_brand ON product_brand.ID = products.BrandID WHERE product_reviews.ID = {id};";

                var result = SqlDataAccess.LoadData<VProductReviewModel>(sql);
                foreach (var Review in result)
                {
                    ProductReviewModel reviewing = new()
                    {
                        ID = Review.ID,
                        Product = new()
                        {
                            Name = Review.Name,
                            Price = Review.Price,
                            Description = Review.Decription,
                            Brand = new()
                            {
                                ID = Review.BrandID,
                                Brand = Review.Brand
                            },
                        },
                        User = new()
                        {
                            Firstname = Review.Firstname,
                            Lastname = Review.Lastname,
                        },
                        Comment = Review.comment,
                        Rating = Review.Rating
                    };
                    productReviews.Add(reviewing);
                }
                if (productReviews.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "Could not find the review");
                }
                return productReviews;
                
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpGet("{productID:int}")]
        public ActionResult<List<ProductReviewModel>> GetAllReviewsToOneProduct (int productID)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                productReviews = new();
                string sql;
                sql = $"SELECT product_reviews.ID, product_reviews.`Comment`, product_reviews.Rating, product_reviews.Date, product_reviews.ProductID, products.Name, products.Price, products.Description, products.BrandID, product_brand.Brand, product_reviews.UserID, users.Firstname, users.Lastname FROM product_reviews INNER JOIN users ON product_reviews.UserID = users.ID INNER JOIN products ON product_reviews.ProductID = products.ID INNER JOIN product_brand ON products.BrandID = product_brand.ID WHERE products.ID = {productID};";

                var result = SqlDataAccess.LoadData<VProductReviewModel>(sql);
                foreach (var Review in result)
                {
                    ProductReviewModel reviewing = new()
                    {
                        ID = Review.ID,
                        Product = new()
                        { 
                            ID = Review.ProductID,
                            Name = Review.Name,
                            Price = Review.Price,
                            Description = Review.Decription,
                            Brand = new()
                            {
                                ID = Review.BrandID,
                                Brand = Review.Brand
                            },
                        },
                        User = new()
                        {
                            ID = Review.UserID,
                            Firstname = Review.Firstname,
                            Lastname = Review.Lastname,
                        },
                        Comment = Review.comment,
                        Rating = Review.Rating
                    };
                    productReviews.Add(reviewing);
                }
                if (productReviews.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "There is no product reviews to the product");
                }
                return productReviews;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreateProductReview(VProductReviewModel createProductReview)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                //VProductReviewModel data = new()
                //{
                //    ProductID = ProductID,
                //    UserID = UserID,
                //    comment = Comment,
                //    Rating = Rating,
                //};

                string sql;
                sql = @"INSERT INTO product_reviews (ProductID,UserID, Comment, Rating) VALUES (@ProductID, @UserID, @comment, @Rating);";

                var result = SqlDataAccess.SaveData<VProductReviewModel>(sql, createProductReview);

                return Ok("Review has been added");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "Create failed because of invalid input");
            }
        }

        [HttpPut]
        public ActionResult UpdateProductReview(VProductReviewModel updateProductReview)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"UDPATE product_reviews SET Comment='{updateProductReview.comment}', Rating = {updateProductReview.Rating} WHERE ID = {updateProductReview.ID}";
                
                var result = SqlDataAccess.UpdateData(sql);

                return Ok("Updated the review succesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "Update failed because review was not found in the database");
            }
        }

        [HttpDelete]
        public ActionResult DeleteOneReview(int id, int userID)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;

                // When checking for permissions
                if (false)
                {

                }
                else
                {
                    sql = $"DELETE FROM product_reviews WHERE ID = {id} AND UserID = {userID}";
                    var result = SqlDataAccess.DeleteData(sql);

                    return Ok("Deleted your review succesfully");
                }

            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "could not delete the review because we could not find it review");
            }
        }
    }
}
