using Microsoft.AspNetCore.Mvc;
using MultiShop.DAL;
using MultiShop.Models;

namespace MultiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly MultiShopDbContext _context = new();
        public IActionResult Index()
        {
            HomeMV home = new()
            {
                products = _context.Products.ToList(),
                categories = _context.Categories.ToList()
            };

            return View(home);
        }
    }
}

