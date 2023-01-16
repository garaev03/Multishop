using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Dtos.AppUserDtos;
using MultiShop.Models;
using NuGet.Protocol;
using System.Linq;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            //_roleManager = roleManager;
            _userManager = userManager;
        }
        //
        public async Task<IActionResult> Index()
        {
            List<UsersGetDto> getDto = await _userManager.Users.Select(u => new UsersGetDto()
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Id = u.Id.ToString(),
                PhoneNumber = u.PhoneNumber
            }).ToListAsync();
            return View(getDto);
        }

        //public async Task<IActionResult> Index()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
        //    await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole() { Name = "Owner" });

        //    return Json("created");
        //}
    }
}
