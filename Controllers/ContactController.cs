using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
