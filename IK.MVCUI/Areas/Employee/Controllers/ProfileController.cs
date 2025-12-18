using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IWebHostEnvironment _env;

        public ProfileController(
            UserManager<AppUser> userManager,
            IEmployeeManager employeeManager,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _employeeManager = employeeManager;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var employee = _employeeManager.GetActives().FirstOrDefault(x => x.AppUserId == user.Id);
            if (employee == null)
                return RedirectToAction("Login", "Account");

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IK.ENTITIES.Models.Employee model, IFormFile? ProfilePhoto)
        {
            var employee = await _employeeManager.GetByIdAsync(model.Id);
            if (employee == null) return NotFound();

            // Bilgileri güncelle
            employee.PhoneNumber = model.PhoneNumber;
            employee.Address = model.Address;

            // FOTOĞRAF YÜKLENDİ Mİ?
            if (ProfilePhoto != null && ProfilePhoto.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images/employees");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePhoto.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePhoto.CopyToAsync(stream);
                }

                employee.ImagePath = "/images/employees/" + uniqueName;
            }

            await _employeeManager.UpdateAsync(employee);

            TempData["Success"] = "Profil bilgileriniz güncellendi.";

            return RedirectToAction("Index");
        }
    }
}
