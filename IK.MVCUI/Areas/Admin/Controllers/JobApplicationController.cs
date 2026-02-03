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
using IK.MVCUI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly IDepartmantManager _departmantManager;
        private readonly IBranchManager _branchManager;



        public JobApplicationController(UserManager<AppUser> userManager, IEmployeeHireService employeeHireService, IHttpClientFactory clientFactory, IPositionManager positionManager, IJobApplicationManager jobApplicationManager, IDepartmantManager departmantManager, IBranchManager branchManager)
        {
            _clientFactory = clientFactory;
            _userManager = userManager;
            _employeeHireService = employeeHireService;
            _positionManager = positionManager;
            _jobApplicationManager = jobApplicationManager;
            _departmantManager = departmantManager;
            _branchManager = branchManager;
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

            var vm = new JobApplicationDetailsPageVm
            {
                JobApplication = app,
                HireVm = new HireEmployeePageVm
                {
                    JobApplicationId = app.Id,
                }
            };

            ViewBag.Branches = new SelectList(await _branchManager.GetAllAsync(),
                "Id",
                "BranchName"
            );

            ViewBag.Departmants = new SelectList(await _departmantManager.GetAllAsync(),
                "Id",
                "DepartmantName"
            );



            return View(vm);
        }

        // 3) ONAYLA
        [HttpPost]
        public async Task<IActionResult> Approve(HireEmployeePageVm vm)
        {

            // 1️ API’den başvuruyu al
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
                UserName = vm.UserName,
                BirthDate = vm.BirthDate,
                Salary = vm.Salary,
                Gender = vm.Gender,
                MaritalStatus = vm.MaritalStatus,
                JobType = vm.JobType,
                BranchId = vm.BranchId,
                DepartmanId = vm.DepartmanId
            };

            var content = new StringContent(JsonConvert.SerializeObject(approveRequest), Encoding.UTF8, "application/json");



            // 3️ API'de status güncelle
            var approveResponse = await ApiClient().PutAsync(
                $"api/JobApplications/{vm.JobApplicationId}/approve",
                content
            );

            if (!approveResponse.IsSuccessStatusCode)
            {
                var apiError = await approveResponse.Content.ReadAsStringAsync();
                TempData["Error"] = apiError;
                //TempData["Error"] = "Başvuru onaylanırken API hatası oluştu.";
                return RedirectToAction("Details", new { id = vm.JobApplicationId });
            }

            var refreshedResponse = await ApiClient().GetAsync($"api/JobApplications/{vm.JobApplicationId}");

            var refreshedJson = await refreshedResponse.Content.ReadAsStringAsync();
            var approvedApplication = JsonConvert.DeserializeObject<JobApplication>(refreshedJson);

            //application.ApplicationStatus = ApplicationStatus.Approved;

            // 4️ Employee oluştur
            await _employeeHireService.HireFromJobApplication(approvedApplication, vm.UserName, vm.DepartmanId, vm.BranchId);

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


