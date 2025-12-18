using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class PayrollController : Controller
    {
        private readonly IPayrollManager _payrollManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly UserManager<AppUser> _userManager;

        public PayrollController(
            IPayrollManager payrollManager,
            IEmployeeManager employeeManager,
            UserManager<AppUser> userManager)
        {
            _payrollManager = payrollManager;
            _employeeManager = employeeManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Login olan user
            var user = await _userManager.GetUserAsync(User);

            // User'a bağlı employee
            var employee = _employeeManager
                .GetActives()
                .FirstOrDefault(x => x.AppUserId == user.Id);

            if (employee == null)
                return Unauthorized();

            // Sadece kendi bordroları
            var payrolls = await _payrollManager
                .GetPayrollsByEmployeeIdAsync(employee.Id);

            return View(payrolls);
        }
    }
}
