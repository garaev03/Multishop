using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MultiShop.DAL;
using MultiShop.Dtos.CategoryDtos;
using MultiShop.Models;
using MultiShop.Services.Implementations;
using MultiShop.Services.Interfaces;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly MultiShopDBContext _db;
        private readonly IWebHostEnvironment _env;


        public CategoriesController(MultiShopDBContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Category> dbCategories = _db.Categories.Where(c => !c.isDeleted);
            List<CategoryGetDto> getCategories = await dbCategories.Select(c => new CategoryGetDto { Name = c.Name, Id = c.Id, Image = c.Image }).ToListAsync();
            return View(getCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDto categoryPostDto)
        {
            if (!ModelState.IsValid)
                return View();

            if (await _db.Categories.AnyAsync(c => c.Name == categoryPostDto.Name))
            {
                ModelState.AddModelError("Name", "Name already exists!");
                return View(categoryPostDto);
            }

            CategoryService categoryService = new CategoryService();
            if (!categoryService.CheckImageExistence(categoryPostDto.formFile))
            {
                ModelState.AddModelError("formFile", "Please, choose only images!");
                return View(categoryPostDto);
            }
            if (!categoryService.ImageValidation(categoryPostDto.formFile))
            {
                ModelState.AddModelError("formFile", "Please, choose only images!");
                return View(categoryPostDto);
            }
            if (!categoryService.CheckImageSize(categoryPostDto.formFile, 2))
            {
                ModelState.AddModelError("formFile", "Please, choose image under 2MB!");
                return View(categoryPostDto);
            }

            string FileName = $"{Guid.NewGuid()}-{categoryPostDto.formFile.FileName}";
            categoryService.CreateImage(_env.WebRootPath, FileName, categoryPostDto.formFile);

            await _db.Categories.AddAsync(new Category { Name = categoryPostDto.Name, Image = FileName });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _db.Categories.FindAsync(id);
            category.isDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Category category = await _db.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            CategoryGetDto GetDto = new() { Id = category.Id, Name = category.Name, Image = category.Image };
            return View(new CategoryUpdateDto() { categoryGetDto = GetDto });
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            CategoryPostDto post = categoryUpdateDto.categoryPostDto;
            if (!ModelState.IsValid)
                return View();

            if (await _db.Categories.AnyAsync(c => c.Name == post.Name))
            {
                ModelState.AddModelError("Name", "Name already exists!");
                return View(post);
            }

            Category? category = await _db.Categories.FindAsync(categoryUpdateDto.categoryGetDto.Id);
            if (category != null)
            {
                CategoryService categoryService = new CategoryService();
                if (categoryService.CheckImageExistence(post.formFile))
                {
                    if (!categoryService.ImageValidation(post.formFile))
                    {
                        ModelState.AddModelError("formFile", "Please, choose only images!");
                        return View(post);
                    }
                    if (!categoryService.CheckImageSize(post.formFile, 2))
                    {
                        ModelState.AddModelError("formFile", "Please, choose image under 2MB!");
                        return View(post);
                    }
                    string FileName = $"{Guid.NewGuid()}-{post.formFile.FileName}";
                    categoryService.CreateImage(_env.WebRootPath, FileName, post.formFile);

                    category.Image = FileName;
                }
                category.Name = post.Name;
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
