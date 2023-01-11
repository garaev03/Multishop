using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.CategoryDtos;
using MultiShop.Dtos.ProductDtos;
using MultiShop.Models;
using MultiShop.Services.Implementations;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MultiShopDBContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductsController(MultiShopDBContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.Where(c => !c.isDeleted).ToListAsync();
            ViewBag.Sizes = await _db.Sizes.Where(c => !c.isDeleted).ToListAsync();
            ViewBag.Colors = await _db.Colors.Where(c => !c.isDeleted).ToListAsync();
            return View(new ProductPostDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto productPostDto)
        {
            ViewBag.Categories = await _db.Categories.Where(c => !c.isDeleted).ToListAsync();
            ViewBag.Sizes = await _db.Sizes.Where(c => !c.isDeleted).ToListAsync();
            ViewBag.Colors = await _db.Colors.Where(c => !c.isDeleted).ToListAsync();

            //if (!ModelState.IsValid)
            //    return View(productPostDto);
            if (productPostDto.formFiles.Count == 0)
            {
                ModelState.AddModelError("formFiles", "Enter at least 1 image!");
                return View(productPostDto);
            }

            List<List<int>> colors = new();
            List<int> colorList = new();
            HashSet<int> colorSet = new();
            foreach (var id in productPostDto.ColorIds)
            {
                if (id == -1)
                {
                    colors.Add(colorList);
                    colorList = new();
                    continue;
                }
                colorList.Add(id);
                if(!colorSet.Contains(id))
                {
                    colorSet.Add(id);
                }
            }
            if(colors.Count!= productPostDto.SizeIds.Count)
            {
                ModelState.AddModelError("", "Size color cannot be empty!");
                return View(productPostDto);
            }

            ImageService service = new();
            List<Image> images = new List<Image>();
            string error = "";
            productPostDto.formFiles.ForEach(formfile =>
            {
                if (!service.CheckImageExistence(formfile))
                {
                    error = "Enter at least 1 image!";
                    ModelState.AddModelError("formFiles", error);
                };
                if (!service.ImageValidation(formfile))
                {
                    error = "Enter only images!";
                    ModelState.AddModelError("formFiles", error);
                }
                if (!service.CheckImageSize(formfile, 2))
                {
                    error = "Enter images under 2MB!!";
                    ModelState.AddModelError("formFiles", error);
                };
                string FolderPath = "assets/img/product-images";
                string FileName = $"{productPostDto.Name}-{Guid.NewGuid()}-{formfile.FileName}";
                service.CreateImage(_env.WebRootPath, FolderPath, FileName, formfile);

                Image image = new()
                {
                    Path= FileName,
                    isDeleted= false,
                    isMain= false,
                };
                if (productPostDto.formFiles.IndexOf(formfile)==0)
                    image.isMain=true;

                images.Add(image);
            });


            Category? category = _db.Categories.Find(productPostDto.CategoryId);
            if (category == null)
                return NotFound();

            Product product = new()
            {
                Name = productPostDto.Name,
                Information = productPostDto.Information,
                Price = productPostDto.Price,
                Title = productPostDto.Title,
                Description = productPostDto.Description,
                MainImage = "loco",
                isDeleted = false,
                Images= images,
                Category=category,
            };
            _db.Products.Add(product);
            _db.SaveChanges();
            //_db.Products.Local[0]
            ProductSize PS = new();
            foreach (var sizeid in productPostDto.SizeIds)
            {
                Size? size= _db.Sizes.Find(sizeid);
                if (size == null)
                    return NotFound();
                PS.Product = product;
                PS.Size = size;
            }

           
            ProductColor PC = new();
            foreach (var colorid in productPostDto.ColorIds)
            {
                if (colorid != -1)
                {
                    Color? color = _db.Colors.Find(colorid);
                    if (color == null)
                        return NotFound();
                    PC.Product = product;
                    PC.Color = color;
                    _db.ProductColors.Add(PC);
                }
            }


            _db.ProductSizes.Add(PS);

            return View();
        }
    }
}
