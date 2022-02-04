namespace Vextech_API.Models.ViewModels
{
    public class VStorageModel
    {
        public ulong ID { get; set; }
        public int StorageCatID { get; set; }
        public string Category { get; set; }
        public int AddressID { get; set; }
        public string Address { get; set; }
        public int PostNumberID { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
    }
}
