namespace Vextech_API.Models
{
    public class StorageModel
    {
        public ulong ID { get; set; }
        public StorageCategoryModel StorageCat { get; set; }
        public AddressModel Address { get; set; }
    }
}
