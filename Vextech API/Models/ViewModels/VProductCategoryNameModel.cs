namespace Vextech_API.Models.ViewModels
{
    public class VProductCategoryNameModel : IProductCategoryNameModel
    {
        public string Category { get; set; }

        public int ID { get; }

        public int? Subcategory { get; set; }
    }
}
