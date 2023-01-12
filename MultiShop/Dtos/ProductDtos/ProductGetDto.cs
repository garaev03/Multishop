using MultiShop.Models;

namespace MultiShop.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string MainImage { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public int TotalCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductSizeColor> ProductSizeColors { get; set; }
        public ProductGetDto()
        {
            Category = new();
            ProductSizeColors = new();
        }
    }
}
