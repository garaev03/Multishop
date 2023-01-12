using MultiShop.Dtos.CategoryDtos;
using MultiShop.Dtos.ProductDtos;
using MultiShop.Models;

namespace MultiShop.ViewModels
{
    public class HomeVM
    {
        public List<CategoryGetDto> Categories { get; set; }
        public List<ProductGetDto> Products { get; set; }

        public HomeVM()
        {
            Categories = new();
            Products = new();
        }
    }
}
