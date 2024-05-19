using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels.Account;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new AppUser
            {
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(vm);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByNameAsync(vm.UserNameOrEmail) ??
                       await _userManager.FindByEmailAsync(vm.UserNameOrEmail);

            if (user == null || !await _userManager.CheckPasswordAsync(user, vm.Password))
            {
                ModelState.AddModelError(string.Empty, "Wrong Password or User Name");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, $"Too many attempts. Locked out until {user.LockoutEnd.Value:HH:mm:ss}");
                return View(vm);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
