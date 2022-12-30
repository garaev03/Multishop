using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
