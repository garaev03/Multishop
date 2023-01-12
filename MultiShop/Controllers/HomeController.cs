using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.CategoryDtos;
using MultiShop.Dtos.ProductDtos;
using MultiShop.Models;
using MultiShop.ViewModels;

namespace MultiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly MultiShopDBContext _db;

        public HomeController(MultiShopDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Category> dbCategories = _db.Categories.Where(c => !c.isDeleted).Include(c => c.Products.Where(p => !p.isDeleted));
            IQueryable<Product> dbProducts = _db.Products.Where(c => !c.isDeleted)
                .Include(p => p.ProductSizeColors
                    .Where(psc => !psc.color.isDeleted && !psc.size.isDeleted))
                .Include(p => p.Images);
            HomeVM home = new()
            {
                Categories = await dbCategories.Select(c => new CategoryGetDto { Name = c.Name, Id = c.Id, Image = c.Image, Products = c.Products }).ToListAsync(),
                Products = await dbProducts.Select(p => new ProductGetDto
                {
                    Category = p.Category,
                    MainImage = p.Images.FirstOrDefault(i=>i.isMain).Path,
                    Name=p.Name,
                    ProductSizeColors = p.ProductSizeColors
                })
                .ToListAsync()
            };
            return View(home);
        }
    }
}
