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

        public async Task<IActionResult> Index()
        {
            IQueryable<Product> dbProducts = _db.Products
                .Include(i=>i.Images)
                .Where(c => !c.isDeleted);
            List<ProductGetDto> getProducts = await dbProducts.Select(c => new ProductGetDto { Name = c.Name, Id = c.Id, Price=c.Price, TotalCount=c.TotalCount, MainImage=c.Images.FirstOrDefault(i=>i.isMain).Path}).ToListAsync();
            return View(getProducts);
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

            if (!ModelState.IsValid)
                return View(productPostDto);
            try
            {
                List<int> UniqueColors = productPostDto.ColorIds.Distinct().ToList();
                List<int> UniqueSizes = productPostDto.SizeIds.Distinct().ToList();
                UniqueColors.Remove(-1);

                ProductService service = new();

                List<List<int>> colors = service.SplitColors(productPostDto.ColorIds);
                service.CheckColors(colors, productPostDto.SizeIds.Count);
                List<Image> images = service.CreateImage(productPostDto.formFiles, productPostDto.Name, _env);

                Category? category = _db.Categories.Find(productPostDto.CategoryId);
                if (category == null)
                    return NotFound();

                int totalCount = 0;
                foreach (int count in productPostDto.Counts)
                {
                    totalCount += count;
                }

                Product product = new()
                {
                    Name = productPostDto.Name,
                    Information = productPostDto.Information,
                    Price = productPostDto.Price,
                    Title = productPostDto.Title,
                    Description = productPostDto.Description,
                    isDeleted = false,
                    TotalCount= totalCount,
                    Images = images,
                    Category = category,
                };
                _db.Products.Add(product);
                _db.SaveChanges();

                foreach (var sizeId in UniqueSizes)
                {
                    Size? size = _db.Sizes.Find(sizeId);
                    if (size == null)
                        return NotFound();
                    ProductSize PS = new();
                    PS.Product = product;
                    PS.Size = size;
                    _db.ProductSizes.Add(PS);
                }

                foreach (var colorId in UniqueColors)
                {
                    Color? color = _db.Colors.Find(colorId);
                    if (color == null)
                        return NotFound();
                    ProductColor PC = new();
                    PC.Product = product;
                    PC.Color = color;
                    _db.ProductColors.Add(PC);
                }

                for (int i = 0; i < productPostDto.SizeIds.Count; i++)
                {
                    Size? size = _db.Sizes.Find(productPostDto.SizeIds[i]);
                    if (size == null)
                        return NotFound();
                    for (int j = 0; j < colors[i].Count; j++)
                    {
                        Color? color = _db.Colors.Find(colors[i][j]);
                        if (color == null)
                            return NotFound();
                        ProductSizeColor PSC = new();
                        PSC.product = product;
                        PSC.size = size;
                        PSC.color = color;
                        PSC.Count = productPostDto.Counts[i];
                        _db.ProductSizeColors.Add(PSC);
                    }
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
