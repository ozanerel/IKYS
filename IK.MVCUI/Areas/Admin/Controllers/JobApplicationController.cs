using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobApplicationController : Controller
    {
        private readonly IJobApplicationManager _jobApplicationManager;
        private readonly IPositionManager _positionManager;

        public JobApplicationController(
            IJobApplicationManager jobApplicationManager,
            IPositionManager positionManager)
        {
            _jobApplicationManager = jobApplicationManager;
            _positionManager = positionManager;
        }

        // 1) TÜM BAŞVURULARI LİSTELE
        public async Task<IActionResult> Index()
        {
            var applications = await _jobApplicationManager.GetAllAsync();
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
        public async Task<IActionResult> Approve(int id)
        {
            await _jobApplicationManager.ApproveApplicationAsync(id);
            return RedirectToAction("Details", new { id });
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
