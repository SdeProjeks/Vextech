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
    public class StorageManagementController : ControllerBase
    {
        public List<StorageProductModel> ProductQuantity { get; set; }

        [HttpGet("{id:int}")]
        public ActionResult<List<StorageProductModel>> GetStorageProducts(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                ProductQuantity = new();
                string sql = $"SELECT products.Name, storage_products.Quantity FROM storage_products INNER JOIN products ON storage_products.ProductID = products.ID WHERE storage_products.StorageID = {id};";
                var result = SqlDataAccess.LoadData<VStorageProductModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid input please change your input and try again.");
                }

                foreach (var item in result)
                {
                    StorageProductModel model = new()
                    {
                        Product = new()
                        {
                            Name = item.Name
                        },
                        Quantity = item.Quantity,
                    };

                    ProductQuantity.Add(model);
                }

                return ProductQuantity;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public ActionResult AddProductToStorage(VStorageProductModel addProductToStorage)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                //VStorageProductModel data = new()
                //{
                //    StorageID = storageID,
                //    ProductID = productID,
                //    Quantity = count
                //};

                string sql = @"INSERT INTO storage_products (StorageID,ProductID,Quantity) VALUES (@StorageID, @ProductID, @Quantity)";
                var result = SqlDataAccess.SaveData<VStorageProductModel>(sql, addProductToStorage);

                return this.StatusCode(StatusCodes.Status201Created,"Product was successfully added to the storage");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid input please change your input and try again.");
            }
        }

        [HttpPut]
        public ActionResult UpdateProductCountForStorage(VStorageProductModel updateProductCount)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                //VStorageProductModel data = new()
                //{
                //    StorageID = storageID,
                //    ProductID = productID,
                //    Quantity = count
                //};

                string sql = @"UPDATE storage_products SET Quantity=@Quantity WHERE StorageID=@StorageID AND ProductID=@ProductID;";
                var result = SqlDataAccess.SaveData<VStorageProductModel>(sql, updateProductCount);

                return Ok("Product quantity updated for the storage successfully.");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the product to update in the database nothing was updated.");
            }
        }


        [HttpDelete]
        public ActionResult DeleteProductFromStorage(VStorageProductModel deleteProduct)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql = $"DELETE FROM storage_products WHERE StorageID={deleteProduct.StorageID} AND ProductID={deleteProduct.ProductID};";
                var result = SqlDataAccess.DeleteData(sql);

                return Ok("Product has been removed from the storage.");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the product to delete in the database nothing was deleted.");
            }
        }
    }
}
