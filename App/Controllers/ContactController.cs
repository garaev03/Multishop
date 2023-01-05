using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;

namespace MultiShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly MultiShopDbContext _context;

        public ContactController(MultiShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Message message)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return View(message);
        }
    }
}
