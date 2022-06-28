using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resto_Backend.Helpers;
using Resto_Backend.Models;
using Resto_Backend.ViewModels;
using System;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Resto_Backend.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        //123456Aa!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = new AppUser()
            {
                Fullname = registerVM.Fullname,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, Roles.Member.ToString());
            await _signInManager.SignInAsync(appUser, true);
            return RedirectToAction("index", "home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser dbUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (dbUser == null)
            {
                ModelState.AddModelError("", "Ya email ya da Password sehvdir");
                return View(loginVM);
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(dbUser, loginVM.Password, loginVM.RememberMe, true);


            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account Is Lock Out");
                return View(loginVM);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Ya Email ya da Password sehvdir");
                return View(loginVM);
            }
            if (returnurl == null)
            {
                return RedirectToAction("index", "home");
            }
            foreach (var item in await _userManager.GetRolesAsync(dbUser))
            {
                if (item.Contains(Roles.Admin.ToString()))
                {
                    return RedirectToAction("index", "Dashboard", new { area = "AdminF" });
                }
            }
            return Redirect(returnurl);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
        }
    }
}
