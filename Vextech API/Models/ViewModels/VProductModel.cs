namespace Vextech_API.Models.ViewModels
{
    public class VProductModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime Release_date { get; set; }
        public int BrandID { get; set; }
        public string Brand { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
