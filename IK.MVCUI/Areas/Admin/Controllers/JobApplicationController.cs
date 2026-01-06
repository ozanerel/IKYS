using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IK.BLL.Managers.Concretes;
using Microsoft.AspNetCore.Identity;
using IK.ENTITIES.Models;
using IK.BLL.Services.Concretes;
using IK.BLL.Services.Abstracts;
using IK.MVCUI.Areas.Admin.Models.PageVms;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobApplicationController : Controller
    {
        private readonly IJobApplicationManager _jobApplicationManager;
        private readonly IPositionManager _positionManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeHireService _employeeHireService;


        public JobApplicationController(
            IJobApplicationManager jobApplicationManager,
            IPositionManager positionManager, IEmployeeManager employeeManager, UserManager<AppUser> userManager, IEmployeeHireService employeeHireService)
        {
            _jobApplicationManager = jobApplicationManager;
            _positionManager = positionManager;
            _employeeManager = employeeManager;
            _userManager = userManager;
            _employeeHireService = employeeHireService;
        }

        // 1) TÜM BAŞVURULARI LİSTELE
        public async Task<IActionResult> Index(ApplicationStatus? status)
        {
            var applications = await _jobApplicationManager.GetAllAsync();

            if (status.HasValue)
            {
                applications = applications
                    .Where(x => x.ApplicationStatus == status.Value)
                    .ToList();
            }

            ViewBag.SelectedStatus = status;

            return View(applications);
        }

        // 2) DETAY SAYFASI
        public async Task<IActionResult> Details(int id)
        {
            var app = await _jobApplicationManager.GetByIdAsync(id);
            if (app == null) return NotFound();

            return View(app);
        }

        // 3) ONAYLA
        [HttpPost]
        public async Task<IActionResult> Approve(JobApplication model)
        {
            var app = await _jobApplicationManager.GetByIdAsync(model.Id);
            if (app == null) return NotFound();

            // Admin tarafından doldurulan alanlar
            app.TCKN = model.TCKN;
            app.BirthDate = model.BirthDate;
            app.Salary = model.Salary;
            app.Gender = model.Gender;
            app.MaritalStatus = model.MaritalStatus;
            app.JobType = model.JobType;

            await _jobApplicationManager.UpdateAsync(app);
            await _jobApplicationManager.ApproveApplicationAsync(app.Id);
            await _employeeHireService.HireFromJobApplication(app);

            return RedirectToAction("Details", new { id = app.Id });
        }


        // 4) REDDET
        public async Task<IActionResult> Reject(int id)
        {
            await _jobApplicationManager.RejectApplicationAsync(id);
            return RedirectToAction("Details", new { id });
        }

        // 5) PENDING YAP
        public async Task<IActionResult> Pending(int id)
        {
            await _jobApplicationManager.SetPendingAsync(id);
            return RedirectToAction("Details", new { id });
        }



    }
}
