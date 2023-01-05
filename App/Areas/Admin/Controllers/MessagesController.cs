using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessagesController : Controller
    {
        private readonly MultiShopDbContext _context;

        public MessagesController(MultiShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Message> messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

    }
}
