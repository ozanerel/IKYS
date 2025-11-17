using IK.DAL.ContextClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        public HomeController(MyContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalEmployees = await _context.Employees.CountAsync();
            return View();
        }
    }
}
