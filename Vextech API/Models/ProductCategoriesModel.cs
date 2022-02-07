namespace Vextech_API.Models
{
    public class ProductCategoriesModel : IProductCategoriesModel
    {
        public ProductModel ProductID { get; set; }
        public ProductCategoryNameModel CategoryID { get; set; }
    }
}
