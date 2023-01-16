using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.ProductDtos;
using MultiShop.Models;
using MultiShop.Services.Implementations;
using System.Data;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
                .Include(psc=>psc.ProductSizeColors)
                .Where(c => !c.isDeleted);
            List<ProductGetDto> getProducts = await dbProducts.Select(c => new ProductGetDto { Name = c.Name, Id = c.Id, TotalCount=c.TotalCount, MainImage=c.Images.FirstOrDefault(i=>i.isMain).Path}).ToListAsync();
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
                ProductService service = new();

                List<List<int>> colors = service.SplitColors(productPostDto.ColorIds);
                service.CheckColors(colors, productPostDto.SizeIds.Count);
                service.CheckPrices(productPostDto.Prices);
                service.CheckCounts(productPostDto.Counts);
                List<Image> images = service.CreateImage(productPostDto.formFiles, productPostDto.Name, _env);

                Category? category = await _db.Categories.FindAsync(productPostDto.CategoryId);
                if (category == null)
                    return NotFound();
                int totalCount = service.GetTotalCount(productPostDto.Counts);

                Product product = new()
                {
                    Name = productPostDto.Name,
                    Information = productPostDto.Information,
                    Title = productPostDto.Title,
                    Description = productPostDto.Description,
                    isDeleted = false,
                    TotalCount= totalCount,
                    Images = images,
                    Category = category,
                };
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();

                for (int i = 0; i < productPostDto.SizeIds.Count; i++)
                {
                    Size? size = await _db.Sizes.FindAsync(productPostDto.SizeIds[i]);
                    if (size == null)
                        return NotFound();
                    for (int j = 0; j < colors[i].Count; j++)
                    {
                        Color? color = await _db.Colors.FindAsync(colors[i][j]);
                        if (color == null)
                            return NotFound();
                        ProductSizeColor PSC = new();
                        PSC.product = product;
                        PSC.size = size;
                        PSC.color = color;
                        PSC.Count = productPostDto.Counts[i];
                        PSC.Price = productPostDto.Prices[i];
                        await _db.ProductSizeColors.AddAsync(PSC);
                    }
                }
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product? Product = await _db.Products.FindAsync(id);
            if(Product == null) return NotFound();
            Product.isDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
