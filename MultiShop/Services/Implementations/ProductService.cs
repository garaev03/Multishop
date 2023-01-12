using MultiShop.Dtos.ProductDtos;
using MultiShop.Models;
using MultiShop.Services.Interfaces;

namespace MultiShop.Services.Implementations
{
    public class ProductService:IProductService, IColorService
    {
        public void CheckColors(List<List<int>> Colors, int SizeCount)
        {
            if (Colors.Count != SizeCount)
                throw new Exception("Color cannot be empty!");
            Colors.ForEach(c =>
            {
                if (c.Count == 0)
                    throw new Exception("Color cannot be empty!");
            });
        }

        public List<Image> CreateImage(List<IFormFile> formFiles, string ProductName,IWebHostEnvironment _env)
        {
            ImageService service = new();
            List<Image> images = new List<Image>();
            if (formFiles.Count == 0)
                throw new Exception("Enter at least 1 image!");
            formFiles.ForEach(formfile =>
            {
                if (!service.CheckImageExistence(formfile))
                    throw new Exception("Enter at least 1 image!");
                if (!service.ImageValidation(formfile))
                    throw new Exception("Enter only images!");
                if (!service.CheckImageSize(formfile, 2))
                    throw new Exception("Enter images under 2MB!");

                string FolderPath = "assets/img/product-images";
                string FileName = $"{ProductName}-{Guid.NewGuid()}-{formfile.FileName}";
                service.CreateImage(_env.WebRootPath, FolderPath, FileName, formfile);

                Image image = new()
                {
                    Path = FileName,
                    isDeleted = false,
                    isMain = false,
                };
                if (formFiles.IndexOf(formfile) == 0)
                    image.isMain = true;

                images.Add(image);
            });
            return images;
        }

        public List<List<int>> SplitColors(List<int> ColorIds)
        {
            List<List<int>> colors = new();
            List<int> colorList = new();
            foreach (var id in ColorIds)
            {
                if (id == -1)
                {
                    if (colorList.Count != 0)
                    {
                        colors.Add(colorList);
                        colorList = new();
                    }
                    continue;
                }
                colorList.Add(id);
            }
            return colors;
        }

        public void CheckCounts(List<int> counts)
        {
            foreach (int count in counts)
            {
                if(count<0)
                    throw new Exception("Count cannot be under 0!");
            }
        }

        public void CheckPrices(List<int> prices)
        {
            foreach (int price in prices)
            {
                if (price < 0)
                    throw new Exception("Price cannot be under 0!");
            }
        }

        public int GetTotalCount(List<int> counts)
        {
            int totalCount = 0;
            foreach (int count in counts)
            {
                totalCount += count;
            }
            return totalCount;
        }
    }
}
