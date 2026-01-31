using IK.BLL.Managers.Abstracts;
using IK.MVCUI.Areas.Employee.Models.PageVms;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IWorkHourManager _workHourManager;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(
            IEmployeeManager employeeManager,
            IWorkHourManager workHourManager,
            UserManager<AppUser> userManager)
        {
            _employeeManager = employeeManager;
            _workHourManager = workHourManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // 1) Giriş yapan kullanıcıyı alma işlemi
            var user = await _userManager.GetUserAsync(User);
            //if (user == null) return RedirectToAction("Logout", "Account", new { area = "" });

            // 2) Employee bilgisini Manager üzerinden bulma işlemi
            var employee = (await _employeeManager.GetAllAsync())
                            .FirstOrDefault(e => e.Email == user.Email);

            //if (employee == null)
            //    return RedirectToAction("Logout", "Account", new { area = "" });

            //Çalışma saatlerini alma
            var today = DateTime.Today;
            var fromDate = today.AddDays(-6);

            var workHours = _workHourManager
                .Where(w => w.EmployeeId == employee.Id && w.EntryTime.Date >= fromDate)
                .ToList();

            // 4) 7 gün için boş bir sözlük oluşturma
            var chartLabels = new List<string>();
            var chartData = new List<decimal>();

            for (int i = 0; i < 7; i++)
            {
                var date = fromDate.AddDays(i).Date;
                var hours = workHours
                    .Where(w => w.EntryTime.Date == date)
                    .Sum(w => w.TotalHours);

                chartLabels.Add(date.ToString("dd.MM"));
                chartData.Add(hours);
            }

            // 5) ViewModel 
            var vm = new EmployeeDashboardPageVm
            {
                EmployeeId = employee.Id,
                FullName = $"{employee.FirstName} {employee.LastName}",
                PositionName = employee.Position?.PositionName ?? "Atanmamış",
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                BranchName = employee.Branch?.BranchName ?? "",
                DepartmanName = employee.Departmant?.DepartmantName ?? "",
                ImagePath = employee.ImagePath,
                StartDate = employee.StartDate,

                ChartLabels = chartLabels,
                ChartData = chartData
            };

            return View(vm);
        }
    }
}
