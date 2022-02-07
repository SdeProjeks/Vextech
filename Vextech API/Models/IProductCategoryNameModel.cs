namespace Vextech_API.Models
{
    public interface IProductCategoryNameModel
    {
        string Category { get; set; }
        int ID { get; }
        int? Subcategory { get; set; }
    }
}