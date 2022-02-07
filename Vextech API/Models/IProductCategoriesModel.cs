namespace Vextech_API.Models
{
    public interface IProductCategoriesModel
    {
        ProductCategoryNameModel CategoryID { get; set; }
        ProductModel ProductID { get; set; }
    }
}