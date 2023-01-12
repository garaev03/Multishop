using MultiShop.Models;

namespace MultiShop.Dtos.ProductDtos
{
    public class ProductPostDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Information { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> formFiles { get; set; }
        public List<int> SizeIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> Counts { get; set; }
        public List<int> Prices { get; set; }
        public ProductPostDto()
        {
            formFiles = new();
            SizeIds = new();
            Counts=new();
            Prices = new();
        }
    }
}
