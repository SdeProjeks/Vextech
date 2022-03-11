using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VStorageProductModel
    {
        [Required]
        [RegularExpression(@"^([0-9]){1,12}")]
        public int StorageID { get; set; }
        public int StorageCatID { get; set; }
        public string Category { get; set; }
        public int AddressID { get; set; }
        public string Address { get; set; }
        public int PostNumberID { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]){1,12}", ErrorMessage = "productID must be a number")]
        public int ProductID { get; set;}
        public string Name { get; set;}

        public int Quantity { get; set; }
    }
}
