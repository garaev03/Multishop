using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.ProductDtos;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MultiShopDBContext _db;
        public ProductsController(MultiShopDBContext db)
        {
            _db = db;
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
            return View();
        }
    }
}
