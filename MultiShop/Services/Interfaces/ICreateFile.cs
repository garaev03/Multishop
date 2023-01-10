namespace MultiShop.Services.Interfaces
{
    public interface ICreateFile
    {
        public void CreateImage(string RootPath, string FileName, IFormFile formFile);
    }
}
