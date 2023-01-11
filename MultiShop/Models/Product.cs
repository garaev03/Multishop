namespace MultiShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public bool isDeleted { get; set; }
        public int TotalCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public Product()
        {
            Category = new();
            Images = new();
            ProductSizes = new();
            ProductColors = new();
        }
    }
}
