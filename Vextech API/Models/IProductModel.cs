
namespace Vextech_API.Models
{
    public interface IProductModel
    {
        int Active { get; set; }
        ProductBrandModel Brand { get; set; }
        string Descrption { get; set; }
        int ID { get; }
        string Name { get; set; }
        decimal Price { get; set; }
        DateTime? Release_date { get; set; }
    }
}