using MultiShop.Models;

namespace MultiShop.Dtos.CategoryDtos
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public List<Product> Products { get; set; }
    }
}
