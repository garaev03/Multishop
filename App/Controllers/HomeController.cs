using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;

namespace MultiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly MultiShopDbContext _context ;

        public HomeController(MultiShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeMV home = new()
            {
                categories = await _context.Categories
                .OrderBy(x => x.Id)
                .ToListAsync(),
                products = await _context.Products
                .OrderBy(x => x.Id)
                .ToListAsync(),
            };



            return View(home);
        }
    }
}

