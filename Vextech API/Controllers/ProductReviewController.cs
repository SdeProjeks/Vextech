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
        public ActionResult<ProductReviewModel> GetOneReview(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                productReviews = new();
                string sql;
                sql = $"SELECT product_reviews.ID, product_reviews.ProductID, users.Firstname, users.Lastname, product_reviews.`Comment`, product_reviews.Rating, product_reviews.Date FROM product_reviews INNER JOIN users ON product_reviews.UserID = users.ID INNER JOIN products ON product_reviews.ProductID = products.ID INNER JOIN product_brand ON product_brand.ID = products.BrandID WHERE product_reviews.ID = {id};";

                var result = SqlDataAccess.LoadData<VProductReviewModel>(sql);
                foreach (var Review in result)
                {
                    ProductReviewModel reviewing = new()
                    {
                        ID = Review.ID,
                        ProductID = Review.ProductID,
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
                return productReviews[0];
                
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpGet("{productID:int}")]
        public ActionResult<List<ProductReviewModel>> GetProductReviews (int productID)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                productReviews = new();
                string sql;
                sql = $"SELECT product_reviews.ID, product_reviews.`Comment`, product_reviews.Rating, product_reviews.Date, product_reviews.ProductID, products.Name, products.Price, products.Description, products.BrandID, product_brand.Brand, product_reviews.UserID, users.Firstname, users.Lastname FROM product_reviews INNER JOIN users ON product_reviews.UserID = users.ID INNER JOIN products ON product_reviews.ProductID = products.ID INNER JOIN product_brand ON products.BrandID = product_brand.ID WHERE products.ID = {productID} ORDER BY product_reviews.Date DESC;";

                var result = SqlDataAccess.LoadData<VProductReviewModel>(sql);
                foreach (var Review in result)
                {
                    ProductReviewModel reviewing = new()
                    {
                        ID = Review.ID,
                        User = new()
                        {
                            ID = Review.UserID,
                            Firstname = Review.Firstname,
                            Lastname = Review.Lastname,
                        },
                        Comment = Review.comment,
                        Rating = Review.Rating,
                        Date = Review.Date
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

        [HttpGet]
        public ActionResult<bool> CheckUsersComment(ulong commentID, string session)
        {
            try
            {
                return UserSessionController.CheckIfUsersComment(commentID, session);
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "could not delete the review because we could not find it review");
            }
        }

        [HttpGet]
        public ActionResult<bool> HasUsersCommented(int productID, string session)
        {
            try
            {
                ulong userid = UserSessionController.getUserIDFromSession(session);

                string sql = $"SELECT UserID FROM product_reviews WHERE ProductID={productID} && UserID={userid}";
                var result = SqlDataAccess.LoadData<ProductReviewModel>(sql);

                bool returnedBool = false;

                if (result.Count != 0) returnedBool = true;

                return returnedBool;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "could not delete the review because we could not find it review");
            }
        }

        [HttpPost]
        public ActionResult CreateProductReview(string comment, int rating, int productID, string session)
        {
            try
            {
                ulong userid = UserSessionController.getUserIDFromSession(session);

                string sql = $"SELECT UserID FROM product_reviews WHERE ProductID={productID} && UserID={userid}";
                var result = SqlDataAccess.LoadData<ProductReviewModel>(sql);

                if (result.Count != 0)
                {
                    return this.StatusCode(StatusCodes.Status403Forbidden, "You have already created a comment for this product.");
                }

                if (UserSessionController.SessionPermissionGrant("comments_create",session) == "Granted")
                {
                    LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                    ProductReviewModel data = new()
                    {
                        ProductID = productID,
                        UserID = 1,
                        Comment = comment,
                        Rating = rating,
                    };

                    sql = @"INSERT INTO product_reviews (ProductID,UserID,Comment,Rating) VALUES (@ProductID, @UserID, @comment, @Rating);";

                    SqlDataAccess.SaveData<ProductReviewModel>(sql, data);

                    return Ok("Review has been added");
                }

                return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have access to create comments");

            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "Create failed because of invalid input");
            }
        }

        [HttpPut]
        public ActionResult UpdateProductReview(ulong ID, string comment, int rating, string session)
        {
            try
            {
                if (UserSessionController.SessionPermissionGrant("comments_update_own", session) == "Granted" && UserSessionController.CheckIfUsersComment(ID,session) || UserSessionController.SessionPermissionGrant("comments_update_all", session) == "Granted")
                {
                    LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                    string date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

                    string sql;
                    sql = $"UPDATE product_reviews SET Comment='{comment}',Rating={rating},Date='{date}' WHERE ID = {ID};";

                    var result = SqlDataAccess.UpdateData(sql);

                    return Ok("Updated the review succesfully");
                }

                return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have access to update this comment");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "Update failed because review was not found in the database");
            }
        }

        [HttpDelete]
        public ActionResult DeleteOneReview(ulong ID, int userID, string session)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;

                // When checking for permissions
                if (UserSessionController.SessionPermissionGrant("comments_delete_own", session) == "Granted" && UserSessionController.CheckIfUsersComment(ID, session) || UserSessionController.SessionPermissionGrant("comments_delete_all", session) == "Granted")
                {
                    sql = $"DELETE FROM product_reviews WHERE ID = {ID} AND UserID = {userID}";
                    var result = SqlDataAccess.DeleteData(sql);

                    return Ok("Deleted your review succesfully");
                }

                return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have access to delete this comment");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "could not delete the review because we could not find it review");
            }
        }
    }
}
