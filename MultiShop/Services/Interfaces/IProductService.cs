using MultiShop.Models;

namespace MultiShop.Services.Interfaces
{
    public interface IProductService
    {
        public List<Image>  CreateImage(List<IFormFile> formFiles, string ProductName, IWebHostEnvironment _env);
    }
}
