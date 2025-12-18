using IK.BLL.Managers.Abstracts;
using IK.DAL.ContextClasses;
using IK.ENTITIES.Models;
using IK.MVCUI.Areas.Admin.Models.PageVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        private readonly IBranchManager _branchManager;
        private readonly IPositionManager _positionManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(MyContext context, UserManager<AppUser> userManager, IBranchManager branchManager, IPositionManager positionManager, IEmployeeManager employeeManager)
        {
            _context = context;
            _branchManager = branchManager;
            _positionManager = positionManager;
            _employeeManager = employeeManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            //int totalBranches = (await _branchManager.GetAllAsync()).Count(); //Aktif ve Pasif hepsini sayar
            int totalBranches = _branchManager.GetActives().Count(); //Sadece Aktifleri sayar.
            ViewBag.TotalBranches = totalBranches;

            int totalPositions = _positionManager.GetActives().Count();
            ViewBag.TotalPositions = totalPositions;

            ViewBag.TotalEmployees = await _context.Employees.CountAsync();

            var employees = await _context.Employees
        .Include(e => e.Position)
        .Include(e => e.Branch)
        .ToListAsync();

            // Employee -> HomePageVm dönüşümü
            List<HomePageVm> model = employees.Select(e => new HomePageVm
            {
                EmployeeId = e.Id,
                FullName = e.FirstName + " " + e.LastName,
                PositionName = e.Position != null ? e.Position.PositionName : "Atanmamış",
                BranchName = e.Branch != null ? e.Branch.BranchName : "-",
                StartedDate = e.StartDate,
                Salary = e.Salary
            }).ToList();

            return View(model);


        }
    }
}
