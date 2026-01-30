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
using Newtonsoft.Json;
using System.Text;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobApplicationController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeHireService _employeeHireService;
        private readonly IPositionManager _positionManager;
        private readonly IJobApplicationManager _jobApplicationManager;


        public JobApplicationController(UserManager<AppUser> userManager, IEmployeeHireService employeeHireService, IHttpClientFactory clientFactory, IPositionManager positionManager, IJobApplicationManager jobApplicationManager)
        {
            _clientFactory = clientFactory;
            _userManager = userManager;
            _employeeHireService = employeeHireService;
            _positionManager = positionManager;
            _jobApplicationManager = jobApplicationManager;
        }

        private HttpClient ApiClient()
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5171/");
            return client;
        }

        // 1) TÜM BAŞVURULARI LİSTELE
        public async Task<IActionResult> Index(ApplicationStatus? status)
        {


            var response = await ApiClient().GetAsync("api/JobApplications");
            var json = await response.Content.ReadAsStringAsync();
            //var applications = JsonConvert.DeserializeObject<List<JobApplication>>(json);
            var entityList = JsonConvert.DeserializeObject<List<AdminJobApplicationPageVm>>(json);

            if (status.HasValue)
            {
                entityList = entityList.Where(x => x.ApplicationStatus == status).ToList();
            }

            entityList.ForEach(x => x.PositionName = "-");

            //var vmList = entityList.Select(x => new AdminJobApplicationPageVm
            //{
            //    Id = x.Id,
            //    ApplicantName = x.ApplicantName,
            //    Email = x.Email,
            //    PositionId = x.PositionId,
            //    PositionName = "-", // API’de Position yok
            //    ApplicationStatus = x.ApplicationStatus,
            //}).ToList();

            return View(entityList);
        }

        // 2) DETAY SAYFASI
        public async Task<IActionResult> Details(int id)
        {
            var response = await ApiClient().GetAsync($"api/JobApplications/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var app = JsonConvert.DeserializeObject<JobApplication>(json);



            return View(app);
        }

        // 3) ONAYLA
        [HttpPost]
        public async Task<IActionResult> Approve(HireEmployeePageVm vm)
        {
            #region Eski
            // 1️⃣ MVC DB'den JobApplication'ı çek
            //var jobApplication = await _jobApplicationManager.GetByIdAsync(vm.JobApplicationId);

            //if (jobApplication == null)
            //    return NotFound();

            //if (jobApplication.ApplicationStatus == ApplicationStatus.Approved)
            //{
            //    TempData["Error"] = "Bu başvuru zaten onaylanmış.";
            //    return RedirectToAction("Details", new { id = vm.JobApplicationId });
            //}

            //// 2️⃣ Admin alanlarını doldur
            //jobApplication.TCKN = vm.TCKN;
            //jobApplication.BirthDate = vm.BirthDate;
            //jobApplication.Salary = vm.Salary;
            //jobApplication.Gender = vm.Gender;
            //jobApplication.MaritalStatus = vm.MaritalStatus;
            //jobApplication.JobType = vm.JobType;

            //jobApplication.ApplicationStatus = ApplicationStatus.Approved;
            //await _jobApplicationManager.UpdateAsync(jobApplication);



            //try
            //{
            //    // 3️⃣ API'ye SADECE STATUS güncellemesi gönder
            //    await ApiClient().PutAsync(
            //        $"api/JobApplications/{vm.JobApplicationId}/approve",
            //        null
            //    );

            //    // 4️⃣ MVC tarafında işe alım (EMAIL %100 DOLU)
            //    await _employeeHireService.HireFromJobApplication(jobApplication);

            //    TempData["Success"] = "Başvuru başarıyla onaylandı.";

            //}
            //catch (Exception ex)
            //{
            //    TempData["Error"] = ex.Message;

            //}

            //return RedirectToAction("Details", new { id = vm.JobApplicationId }); 
            #endregion

            #region Eski_2
            //// 1️⃣ API’den başvuruyu al
            //var response = await ApiClient().GetAsync($"api/JobApplications/{vm.JobApplicationId}");
            //if (!response.IsSuccessStatusCode)
            //    return NotFound();

            //var json = await response.Content.ReadAsStringAsync();
            //var application = JsonConvert.DeserializeObject<JobApplication>(json);

            //if (application.ApplicationStatus == ApplicationStatus.Approved)
            //{
            //    TempData["Error"] = "Bu başvuru zaten onaylanmış.";
            //    return RedirectToAction("Details", new { id = vm.JobApplicationId });
            //}

            //// 2️⃣ VM → JobApplication (EmployeeHireService için)
            //application.TCKN = vm.TCKN;
            //application.BirthDate = vm.BirthDate;
            //application.Salary = vm.Salary;
            //application.Gender = vm.Gender;
            //application.MaritalStatus = vm.MaritalStatus;
            //application.JobType = vm.JobType;

            //// 3️⃣ API'de status güncelle
            //var approveResponse = await ApiClient().PutAsync(
            //    $"api/JobApplications/{vm.JobApplicationId}/approve",
            //    null
            //);

            //if (!approveResponse.IsSuccessStatusCode)
            //{
            //    TempData["Error"] = "Başvuru onaylanırken API hatası oluştu.";
            //    return RedirectToAction("Details", new { id = vm.JobApplicationId });
            //}

            ////application.ApplicationStatus = ApplicationStatus.Approved;

            //// 4️⃣ Employee oluştur
            //await _employeeHireService.HireFromJobApplication(application);

            //TempData["Success"] = "Başvuru onaylandı.";

            //return RedirectToAction("Details", new { id = vm.JobApplicationId });

            #endregion

            // 1️⃣ API’den başvuruyu al
            var response = await ApiClient().GetAsync($"api/JobApplications/{vm.JobApplicationId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var application = JsonConvert.DeserializeObject<JobApplication>(json);

            if (application.ApplicationStatus == ApplicationStatus.Approved)
            {
                TempData["Error"] = "Bu başvuru zaten onaylanmış.";
                return RedirectToAction("Details", new { id = vm.JobApplicationId });
            }


            var approveRequest = new
            {
                TCKN = vm.TCKN,
                BirthDate = vm.BirthDate,
                Salary = vm.Salary,
                Gender = vm.Gender,
                MaritalStatus = vm.MaritalStatus,
                JobType = vm.JobType,
            };

            var content = new StringContent(JsonConvert.SerializeObject(approveRequest),Encoding.UTF8,"application/json");
            // 2️⃣ VM → JobApplication (EmployeeHireService için)
            //application.TCKN = vm.TCKN;
            //application.BirthDate = vm.BirthDate;
            //application.Salary = vm.Salary;
            //application.Gender = vm.Gender;
            //application.MaritalStatus = vm.MaritalStatus;
            //application.JobType = vm.JobType;

            // 3️⃣ API'de status güncelle
            var approveResponse = await ApiClient().PutAsync(
                $"api/JobApplications/{vm.JobApplicationId}/approve",
                content
            );

            if (!approveResponse.IsSuccessStatusCode)
            {
                TempData["Error"] = "Başvuru onaylanırken API hatası oluştu.";
                return RedirectToAction("Details", new { id = vm.JobApplicationId });
            }

            //application.ApplicationStatus = ApplicationStatus.Approved;

            // 4️⃣ Employee oluştur
            await _employeeHireService.HireFromJobApplication(application);

            TempData["Success"] = "Başvuru onaylandı.";

            return RedirectToAction("Details", new { id = vm.JobApplicationId });

        }


        // 4) REDDET
        public async Task<IActionResult> Reject(int id)
        {
            await ApiClient().PutAsync($"api/JobApplications/{id}/reject", null);
            return RedirectToAction("Details", new { id });
        }

        // 5) PENDING YAP
        public async Task<IActionResult> Pending(int id)
        {
            await ApiClient().PutAsync($"api/JobApplications/{id}/pending", null);
            return RedirectToAction("Details", new { id });
        }



    }
}


