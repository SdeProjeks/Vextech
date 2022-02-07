namespace Vextech_API.Models.ViewModels
{
    public class VProductCategoriesModel : IProductCategoriesModel
    {
        public ProductCategoryNameModel CategoryID { get; set; }
        public ProductModel ProductID { get; set; }
    }
}
