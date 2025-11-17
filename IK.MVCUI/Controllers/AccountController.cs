using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Kullanıcı adı ve şifre gerekli.";
                return View();
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı.";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!result.Succeeded)
            {
                ViewBag.Error = "Giriş başarısız.";
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            else if (roles.Contains("Employee"))
                return RedirectToAction("Index", "Home", new { area = "Employee" });
            else
                return RedirectToAction("AccessDenied", "Account");
        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied() => Content("Bu alana erişim yetkiniz yok.");
    }
}
