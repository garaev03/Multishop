namespace MultiShop.Services.Interfaces
{
    public interface IImageService
    {
        public bool CheckImageExistence(IFormFile? formFile);
        public bool ImageValidation(IFormFile formFile);    
        public bool CheckImageSize(IFormFile formFile,int size);
        public void CreateImage(string RootPath, string FolderPath, string FileName, IFormFile formFile);
    }
}
