using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.CategoryDtos;
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
            IQueryable<Category> dbCategories = _db.Categories.Where(c => !c.isDeleted);
            HomeVM home = new()
            {
                Categories = await dbCategories.Select(c => new CategoryGetDto { Name = c.Name, Id = c.Id, Image = c.Image }).ToListAsync()
            };
            return View(home);
        }
    }
}
