using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Dtos.SizeDtos;
using MultiShop.Models;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SizesController : Controller
    {
        private readonly MultiShopDBContext _db;
        public SizesController(MultiShopDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<SizeGetDto> sizes = await _db.Sizes.Where(s => !s.isDeleted).Select(s => new SizeGetDto() { Id = s.Id, Value = s.Value }).ToListAsync();
            return View(sizes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SizePostDto sizePostDto)
        {
            if (!ModelState.IsValid)
                return View(sizePostDto);
            if (await _db.Sizes.AnyAsync(s => s.Value == sizePostDto.Value))
            {
                ModelState.AddModelError("Value", "Value already exists!");
                return View(sizePostDto);
            }
            await _db.Sizes.AddAsync(new Size { Value = sizePostDto.Value });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Size? dbSize = await _db.Sizes.FindAsync(id);
            if (dbSize == null)
                return NotFound();

            SizeGetDto GetDto = new() { Id = dbSize.Id, Value = dbSize.Value };
            return View(new SizeUpdateDto() { sizeGetDto = GetDto });
        }
        [HttpPost]
        public async Task<ActionResult> Update(SizeUpdateDto sizeUpdateDto)
        {
            if (!ModelState.IsValid)
                return View(sizeUpdateDto);
            if (await _db.Sizes.AnyAsync(s => s.Value == sizeUpdateDto.sizePostDto.Value))
            {
                ModelState.AddModelError("Value", "Value already exists!");
                return View(sizeUpdateDto);
            }

            Size? size=await _db.Sizes.FindAsync(sizeUpdateDto.sizeGetDto.Id);
            if (size == null)
                return NotFound();
            size.Value = sizeUpdateDto.sizePostDto.Value;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Size? size = await _db.Sizes.FindAsync(id);
            if (size == null)
                return NotFound();
            size.isDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
