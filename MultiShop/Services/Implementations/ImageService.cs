using MultiShop.Dtos.CategoryDtos;
using MultiShop.Services.Interfaces;

namespace MultiShop.Services.Implementations
{
    public class ImageService : IImageService
    {
        public bool CheckImageExistence(IFormFile? formFile)
        {
            if (formFile == null)
                   return false;
            return true;
        }
        public bool CheckImageSize(IFormFile formFile, int size)
        {
            if (formFile.Length / 1024 / 1024 > 2)
                return false;
            return true;
        }
        public bool ImageValidation(IFormFile formFile)
        {
            if (!formFile.ContentType.Contains("image"))
                return false;
            return true;
        }
        public void CreateImage(string RootPath,string FolderPath,string FileName,IFormFile formFile)
        {
            string Fullpath = Path.Combine(RootPath, FolderPath, FileName);
            using (FileStream stream = new(Fullpath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
        }
    }
}
