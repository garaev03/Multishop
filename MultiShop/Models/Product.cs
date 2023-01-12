namespace MultiShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public bool isDeleted { get; set; }
        public int TotalCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public List<ProductSizeColor> ProductSizeColors { get; set; }
        public Product()
        {
            Category = new();
            Images = new();
            ProductSizeColors = new();
        }
    }
}
