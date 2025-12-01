using IK.BLL.Managers.Abstracts;
using IK.DAL.ContextClasses;
using Microsoft.AspNetCore.Authorization;
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
        public HomeController(MyContext context, IBranchManager branchManager,IPositionManager positionManager)
        {
            _context = context;
            _branchManager = branchManager;
            _positionManager = positionManager;
        }

        public async Task<IActionResult> Index()
        {

            //int totalBranches = (await _branchManager.GetAllAsync()).Count(); //Aktif ve Pasif hepsini sayar
            int totalBranches = _branchManager.GetActives().Count(); //Sadece Aktifleri sayar.
            ViewBag.TotalBranches = totalBranches;

            int totalPositions = _positionManager.GetActives().Count();
            ViewBag.TotalPositions = totalPositions;

            ViewBag.TotalEmployees = await _context.Employees.CountAsync();
            return View();
        }
    }
}
