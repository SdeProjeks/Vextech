using Vextech_API.DataAccess;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Models
{
    public class ProductCategoriesModel : IProductCategoriesModel
    {
        public ProductModel ProductID { get; set; }
        public ProductCategoryNameModel CategoryID { get; set; }
    }
}
