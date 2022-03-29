using System.ComponentModel.DataAnnotations.Schema;

namespace Vextech_APP.ViewModels.ProductModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public ProductBrandModel Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Active { get; set; } = 0;
        public decimal Price { get; set; }
        public DateTime? Release_date { get; set; }
        public List<ProductCategoryNameModel> Categories { get; set; } = new();
        public List<ProductImagesViewModel> Images { get; set; } = new();
        public List<ProductReviewViewModel> Reviews { get; set; } = new();
    }
}
