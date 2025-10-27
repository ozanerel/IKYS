using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IJobApplicationManager _jobApplicationManager;
        private readonly IPositionManager _positionManager;
        private readonly IWebHostEnvironment _env;

        public JobApplicationController(
            IJobApplicationManager jobApplicationManager,
            IPositionManager positionManager,
            IWebHostEnvironment env)
        {
            _jobApplicationManager = jobApplicationManager;
            _positionManager = positionManager;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Pozisyonları veritabanından çekiyoruz
            var positions = await _positionManager.GetAllAsync();
            ViewBag.Positions = positions;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(JobApplication model, IFormFile CVFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionManager.GetAllAsync();
                return View(model);
            }

            if (CVFile != null && CVFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/cv");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(CVFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await CVFile.CopyToAsync(fileStream);
                }

                model.CVFilePath = "/uploads/cv/" + uniqueFileName;
            }

            model.ApplicationStatus = ApplicationStatus.Pending;
            model.CreatedDate = DateTime.Now;

            await _jobApplicationManager.CreateAsync(model);

            TempData["Success"] = "Başvurunuz başarıyla alınmıştır!";
            return RedirectToAction("Index");
        }
    }
}
