using IK.DAL.ContextClasses;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IK.MVCUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(MyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var employee = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Email == user.Email);

            //ViewBag.FullName = $"{employee.FirstName} {employee.LastName}";
            //ViewBag.Position = employee.Position?.PositionName ?? "Atanmamış";
            return View(employee);
        }
    }
}
