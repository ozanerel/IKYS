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
            #region Hatayı konsola yazdırma
            //if (!model.PrivacyAccepted)
            //{
            //    ModelState.AddModelError("", "Veri işleme onayı zorunludur.");
            //}



            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState
            //        .Where(x => x.Value.Errors.Count > 0)
            //        .Select(x => new
            //        {
            //            Field = x.Key,
            //            Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
            //        })
            //        .ToList();

            //    // DEBUG İÇİN
            //    foreach (var err in errors)
            //    {
            //        Console.WriteLine($"FIELD: {err.Field}");
            //        foreach (var msg in err.Errors)
            //        {
            //            Console.WriteLine($"ERROR: {msg}");
            //        }
            //    }

            //    ViewBag.Positions = await _positionManager.GetAllAsync();
            //    return View(model);
            //} 
            #endregion

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
            //model.CreatedDate = DateTime.Now;
            model.ApplicateDate = DateTime.Now;

            await _jobApplicationManager.CreateAsync(model);

            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionManager.GetAllAsync();
                return View(model);
            }

            TempData["Success"] = "Başvurunuz başarıyla alınmıştır!";
            return RedirectToAction("Index");
        }
    }
}
