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


        public JobApplicationController(UserManager<AppUser> userManager, IEmployeeHireService employeeHireService, IHttpClientFactory clientFactory,IPositionManager positionManager)
        {
            _clientFactory = clientFactory;
            _userManager = userManager;
            _employeeHireService = employeeHireService;
            _positionManager = positionManager;
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
            var entityList = JsonConvert.DeserializeObject<List<JobApplication>>(json);

            var vmList = entityList.Select(x => new AdminJobApplicationPageVm
            {
                Id = x.Id,
                ApplicantName = x.ApplicantName,
                Email = x.Email,
                PositionId = x.PositionId,
                PositionName = "-", // API’de Position yok
                ApplicationStatus = x.ApplicationStatus,
                //CreatedDate = x.CreatedDate
            }).ToList();

            return View(vmList);


            //if (status.HasValue)
            //{
            //    applications = applications.Where(x => x.ApplicationStatus == status).ToList();
            //}

            //return View(applications);
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

            //var vm = new HireEmployeePageVm
            //{
            //    JobApplicationId = app.Id,
            //    ApplicantName = app.ApplicantName,
            //    Email = app.Email,
            //    PhoneNumber = app.PhoneNumber,
            //    PositionId = app.PositionId
            //};

            return View(app);
        }

        // 3) ONAYLA
        [HttpPost]
        public async Task<IActionResult> Approve(JobApplication model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            await ApiClient().PutAsync($"api/JobApplications/{model.Id}/approve",
            null);

            await _employeeHireService.HireFromJobApplication(model);

            return RedirectToAction("Details", new { id = model.Id });
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
