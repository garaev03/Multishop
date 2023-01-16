using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.ColorDtos;
using MultiShop.Models;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ColorsController : Controller
    {
        private readonly MultiShopDBContext _db;
        public ColorsController(MultiShopDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<ColorGetDto> sizes = await _db.Colors.Where(c => !c.isDeleted).Select(c => new ColorGetDto() { Id = c.Id, Value = c.Value }).ToListAsync();
            return View(sizes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ColorPostDto colorPostDto)
        {
            if (!ModelState.IsValid)
                return View(colorPostDto);
            if (await _db.Colors.AnyAsync(s => s.Value == colorPostDto.Value))
            {
                ModelState.AddModelError("Value", "Value already exists!");
                return View(colorPostDto);
            }
            await _db.Colors.AddAsync(new Color { Value = colorPostDto.Value });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Color? dbColor = await _db.Colors.FindAsync(id);
            if (dbColor == null)
                return NotFound();

            ColorGetDto GetDto = new() { Id = dbColor.Id, Value = dbColor.Value };
            return View(new ColorUpdateDto() { colorGetDto = GetDto });
        }
        [HttpPost]
        public async Task<ActionResult> Update(ColorUpdateDto ColorUpdateDto)
        {
            if (!ModelState.IsValid)
                return View(ColorUpdateDto);
            if (await _db.Colors.AnyAsync(s => s.Value == ColorUpdateDto.colorPostDto.Value))
            {
                ModelState.AddModelError("Value", "Value already exists!");
                return View(ColorUpdateDto);
            }

            Color? color=await _db.Colors.FindAsync(ColorUpdateDto.colorGetDto.Id);
            if (color == null)
                return NotFound();
            color.Value = ColorUpdateDto.colorPostDto.Value;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Color? color = await _db.Colors.FindAsync(id);
            if (color == null)
                return NotFound();
            color.isDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
