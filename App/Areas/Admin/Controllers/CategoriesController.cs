using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MultiShop.DAL;
using MultiShop.Models;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly MultiShopDbContext _context;

        public CategoriesController(MultiShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Main()
        {
            List<Category> categories = await _context.Categories
                .Include(x=>x.Products)
                .OrderBy(x => x.Id)
                .ToListAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool checkExistence= await _context.Categories.AnyAsync(x=>x.Name.Trim().ToLower()==category.Name.Trim().ToLower());
            if (checkExistence)
            {
                ModelState.AddModelError("Name", "Category name already exists!");
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main");
        }


        public  IActionResult Update(int id)
        {
            Category category =  _context.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public  async Task<IActionResult> Update(Category editedCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category category = _context.Categories.Find(editedCategory.Id);

            category.Name = editedCategory.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction("Main");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            Category category = _context.Categories.Find(id);

             _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main");
        }
    }
}
