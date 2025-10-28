using IK.DAL.ContextClasses;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IK.MVCUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
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
            // Giriş yapan kullanıcıyı al
            var user = await _userManager.GetUserAsync(User);

            // Kullanıcının profili ve ilişkili Employee bilgisi
            var employee = await _context.Employees
                .Include(e => e.Branch)
                //.Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.Payrolls)
                .Include(e => e.WorkHours)
                .FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employee == null)
                return RedirectToAction("Login", "Account");

            // Dinamik veriler
            ViewBag.FullName = $"{employee.FirstName} {employee.LastName}";
            ViewBag.Branch = employee.Branch?.BranchName ?? "Atanmamış";
            //ViewBag.Department = employee.Department?.DepartmentName ?? "Atanmamış";
            ViewBag.Position = employee.Position?.PositionName ?? "Atanmamış";
            ViewBag.TotalWorkHours = employee.WorkHours.Sum(x => x.TotalHours);
            ViewBag.TotalSalary = employee.Payrolls.Sum(x => x.NetSalary);

            return View(employee);
        }
    }
}
