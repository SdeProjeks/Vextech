using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;

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

                if (result == 0)
                {
                    this.StatusCode(StatusCodes.Status500InternalServerError, "An error has occoured and your storage category was not created.");
                }


                return Ok("Succes");
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

                if (result == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "The storage category selected does not exist anymore in the database. Nothing got updated");
                }

                return Ok("Succes");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        public List<StorageModel> Remapping { get; set; } = new List<StorageModel>();

        [HttpGet]
        public ActionResult<List<StorageModel>> GetStorages()
        {
            try
            {
                string sql;
                sql = $"SELECT storage.ID, storage.StorageCatID, storage_category.Category, storage.AddressID, addresses.Address, addresses.PostNumberID, post_numbers.PostNumber, post_numbers.City, post_numbers.CountryID, countries.Country FROM storage INNER JOIN storage_category ON storage.ID = storage_category.ID INNER JOIN addresses ON storage.AddressID = addresses.ID INNER JOIN post_numbers ON addresses.PostNumberID = post_numbers.ID INNER JOIN countries ON post_numbers.CountryID = countries.ID;";
                var result = SqlDataAccess.LoadData<VStorageModel>(sql);

                foreach (var storage in result)
                {
                    StorageModel model = new StorageModel()
                    {
                        ID = storage.ID,
                        StorageCat = new StorageCategoryModel()
                        {
                            ID = storage.StorageCatID,
                            Category = storage.Category,
                        },
                        Address = new AddressModel()
                        {
                            ID = storage.AddressID,
                            Address = storage.Address,
                            PostNumber = new post_numbers()
                            {
                                ID = storage.PostNumberID,
                                PostNumber = storage.PostNumber,
                                City = storage.City,
                                Country = new CountryModel()
                                {
                                    ID = storage.CountryID,
                                    Country = storage.Country,
                                }
                            }
                        },
                    };
                    Remapping.Add(model);
                }

                return Remapping;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }


        [HttpGet("{id:int}")]
        public ActionResult<List<StorageModel>> GetStorages(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT storage.ID, storage.StorageCatID, storage_category.Category, storage.AddressID, addresses.Address, addresses.PostNumberID, post_numbers.PostNumber, post_numbers.City, post_numbers.CountryID, countries.Country FROM storage INNER JOIN storage_category ON storage.ID = storage_category.ID INNER JOIN addresses ON storage.AddressID = addresses.ID INNER JOIN post_numbers ON addresses.PostNumberID = post_numbers.ID INNER JOIN countries ON post_numbers.CountryID = countries.ID WHERE storage.ID = {id};";
                var result = SqlDataAccess.LoadData<VStorageModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "The storage selected does not exist.");
                }

                foreach (var storage in result)
                {
                    StorageModel model = new StorageModel()
                    {
                        ID = storage.ID,
                        StorageCat = new StorageCategoryModel()
                        {
                            ID = storage.StorageCatID,
                            Category = storage.Category,
                        },
                        Address = new AddressModel()
                        {
                            ID = storage.AddressID,
                            Address = storage.Address,
                            PostNumber = new post_numbers()
                            {
                                ID = storage.PostNumberID,
                                PostNumber = storage.PostNumber,
                                City = storage.City,
                                Country = new CountryModel()
                                {
                                    ID = storage.CountryID,
                                    Country = storage.Country,
                                }
                            }
                        },
                    };
                    Remapping.Add(model);
                }

                return Remapping;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreateStorage(int categoryID, int addressID)
        {
            try
            {
                VStorageModel data = new VStorageModel()
                {
                    StorageCatID = categoryID,
                    AddressID = addressID
                };

                string sql;
                sql = @"INSERT INTO storage (StorageCatID, AddressID) VALUES (@StorageCatID, @AddressID);";
                var result = SqlDataAccess.SaveData<VStorageModel>(sql, data);

                if (result == 0)
                {
                    this.StatusCode(StatusCodes.Status500InternalServerError, "An error has occoured and your storage category was not created.");
                }


                return Ok("Succes");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        public ActionResult UpdateStorage(int id, int categoryID, int addressID)
        {
            try
            {
                string sql;

                sql = $"UPDATE storage SET StorageCatID={categoryID}, AddressID={addressID} WHERE ID = {id};";
                var result = SqlDataAccess.UpdateData(sql);

                if (result == 0)
                {
                    this.StatusCode(StatusCodes.Status500InternalServerError, "An error has occoured and your storage category was not created.");
                }


                return Ok("Succes");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
