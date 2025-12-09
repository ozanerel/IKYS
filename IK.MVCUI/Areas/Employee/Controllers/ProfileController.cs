using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeManager _employeeManager;

        public ProfileController(UserManager<AppUser> userManager, IEmployeeManager employeeManager)
        {
            _userManager = userManager;
            _employeeManager = employeeManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var employee = _employeeManager
                .GetActives()
                .FirstOrDefault(x => x.AppUserId == user.Id);

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IK.ENTITIES.Models.Employee model)
        {
            var employee = await _employeeManager.GetByIdAsync(model.Id);
            if (employee == null) return NotFound();

            employee.PhoneNumber = model.PhoneNumber;
            employee.Address = model.Address;

            await _employeeManager.UpdateAsync(employee);

            TempData["Success"] = "Profil bilgileriniz güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
