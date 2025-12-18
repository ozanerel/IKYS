using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class WorkHourController : Controller
    {
        private readonly IWorkHourManager _workHourManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeManager _employeeManager;

        public WorkHourController(
            IWorkHourManager workHourManager,
            UserManager<AppUser> userManager,
            IEmployeeManager employeeManager)
        {
            _workHourManager = workHourManager;
            _userManager = userManager;
            _employeeManager = employeeManager;
        }

        // Kayıt Listesi
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var employee = _employeeManager.GetActives().FirstOrDefault(x => x.AppUserId == user.Id);

            var list = _workHourManager
                .GetActives()
                .Where(x => x.EmployeeId == employee.Id)
                .OrderByDescending(x => x.EntryTime)
                .ToList();

            return View(list);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DateTime entryTime, DateTime exitTime)
        {
            var user = await _userManager.GetUserAsync(User);
            var employee = _employeeManager.GetActives().FirstOrDefault(x => x.AppUserId == user.Id);

            if (employee == null)
            {
                TempData["Error"] = "Çalışan bulunamadı.";
                return RedirectToAction("Index");
            }

            await _workHourManager.AddWorkHoursAsync(employee.Id, entryTime, exitTime);

            TempData["Success"] = "Mesai kaydı başarıyla oluşturuldu.";
            return RedirectToAction("Index");
        }
    }
}
