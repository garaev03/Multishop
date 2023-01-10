namespace MultiShop.Services.Interfaces
{
    public interface ICheckImage
    {
        public bool CheckImageExistence(IFormFile? formFile);
        public bool ImageValidation(IFormFile formFile);    
        public bool CheckImageSize(IFormFile formFile,int size);
    }
}
