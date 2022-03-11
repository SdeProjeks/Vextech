using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VStorageModel
    {
        public ulong ID { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]){1,20}")]
        public int StorageCatID { get; set; }
        public string Category { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]){1,20}")]
        public int AddressID { get; set; }
        public string Address { get; set; }
        public int PostNumberID { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
    }
}
