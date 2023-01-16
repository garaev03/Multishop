using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Dtos.AppUserDtos;
using MultiShop.Models;

namespace MultiShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> sign)
        {
            _userManager = userManager;
            _signManager = sign;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser newUser = new()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName,
                Email = register.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                return View(register);
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(login);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signManager.PasswordSignInAsync(user, login.Password, true, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked for 5 minutes.");
                    return View(login);
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Account is not allowed to enter. Please contact with support.");
                    return View(login);
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileUpdateDto profile = new()
            {
                GetDto = new ProfileGetDto()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName= user.LastName,
                    UserName=user.UserName,
                    Password=user.PasswordHash
                }
            };
            return View(profile);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
