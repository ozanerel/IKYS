using IK.BLL.Managers.Abstracts;
using IK.BLL.Managers.Concretes;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using IK.MVCUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Controllers
{
    public class JobApplicationController : Controller
    {
        //private readonly IJobApplicationManager _jobApplicationManager;
        private readonly IPositionManager _positionManager;
        private readonly IWebHostEnvironment _env;
        private readonly IJobApplicationApiService _api;

        public JobApplicationController(
            IJobApplicationApiService api,
            IPositionManager positionManager,
            IWebHostEnvironment env)
        {
            _api = api;
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

            #region Before Api
            //if (CVFile != null && CVFile.Length > 0)
            //{
            //    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/cv");
            //    if (!Directory.Exists(uploadsFolder))
            //        Directory.CreateDirectory(uploadsFolder);

            //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(CVFile.FileName);
            //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await CVFile.CopyToAsync(fileStream);
            //    }

            //    model.CVFilePath = "/uploads/cv/" + uniqueFileName;
            //} 

            //model.ApplicationStatus = ApplicationStatus.Pending;
            ////model.CreatedDate = DateTime.Now;
            //model.ApplicateDate = DateTime.Now;

            //await _jobApplicationManager.CreateAsync(model);
            #endregion

            //var success = await _api.SendApplicationAsync(model,CVFile);

            //if (!success)
            //{
            //    ModelState.AddModelError("","Başvuru gönderilirken hata oluştu");
            //    ViewBag.Positions = await _positionManager.GetAllAsync();
            //    return View(model);
            //}



            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Positions = await _positionManager.GetAllAsync();
            //    return View(model);
            //}

            //TempData["Success"] = "Başvurunuz başarıyla alınmıştır!";
            //return RedirectToAction("Index");


            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionManager.GetAllAsync();
                return View(model);
            }

            try
            {
                using var client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5171/");

                using var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(model.ApplicantName), "ApplicantName");
                formData.Add(new StringContent(model.Email), "Email");
                formData.Add(new StringContent(model.PhoneNumber), "PhoneNumber");
                formData.Add(new StringContent(model.Address), "Address");
                formData.Add(new StringContent(model.PositionId.ToString()), "PositionId");
                formData.Add(new StringContent("true"), "PrivacyAccepted");//Hardcode true gönderdik zaten kullanıcı işaretlemeden submit olamıyor. Aynı zamanda API tarafında da zorunlu.

                if (CVFile != null && CVFile.Length > 0)
                {
                    var streamContent = new StreamContent(CVFile.OpenReadStream());
                    streamContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(CVFile.ContentType);

                    formData.Add(streamContent, "CvFilePath", CVFile.FileName);
                }

                var response = await client.PostAsync("api/JobApplications", formData);

                if (!response.IsSuccessStatusCode)
                {
                    var apiError = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", apiError);

                    ModelState.AddModelError("", "Başvuru gönderilirken hata oluştu");
                    ViewBag.Positions = await _positionManager.GetAllAsync();
                    return View(model);
                }

                TempData["Success"] = "Başvurunuz başarıyla alınmıştır.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Başvuru gönderilirken hata oluştu");
                ViewBag.Positions = await _positionManager.GetAllAsync();
                return View(model);
            }
        }
    }
}
