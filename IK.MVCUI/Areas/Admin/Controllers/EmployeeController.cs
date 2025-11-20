using IK.BLL.Managers.Abstracts;
using IK.MVCUI.Areas.Admin.Models.PageVms;
using Microsoft.AspNetCore.Mvc;
//using IK.ENTITIES.Models;


namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        readonly IEmployeeManager _employeeManager;
        readonly IDepartmantManager _departmantManager;
        readonly IPositionManager _positionManager;
        readonly IBranchManager _branchManager;
        public EmployeeController(IEmployeeManager employeeManager, IDepartmantManager departmantManager, IPositionManager positionManager, IBranchManager branchManager)
        {
            _employeeManager = employeeManager;
            _departmantManager = departmantManager;
            _positionManager = positionManager;
            _branchManager = branchManager;
        }
        public async Task<IActionResult> Index()
        {
            List<IK.ENTITIES.Models.Employee> employees = await _employeeManager.GetAllAsync();
            return View(employees);
        }


        public IActionResult Create()
        {
            EmployeePageVm employeePagevm = new EmployeePageVm()
            {
                Departmants = _departmantManager.GetActives(),
                Positions = _positionManager.GetActives(),
                Branches = _branchManager.GetActives(),
            };
            return View(employeePagevm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeePageVm employeePageVm)
        {

            if (!ModelState.IsValid)
            {
                employeePageVm.Departmants = _departmantManager.GetActives();
                employeePageVm.Positions = _positionManager.GetActives();
                employeePageVm.Branches = _branchManager.GetActives();
                return View(employeePageVm);
            }
            await _employeeManager.CreateAsync(employeePageVm.appUsers.FirstOrDefault());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int Id)
        {
            IK.ENTITIES.Models.Employee employee = await _employeeManager.GetByIdAsync(Id);
            EmployeePageVm employeePageVm = new EmployeePageVm()
            {
                appUsers = new List<IK.ENTITIES.Models.AppUser>() { employee.AppUser },
                Departmants = _departmantManager.GetActives(),
                Positions = _positionManager.GetActives(),
                Branches = _branchManager.GetActives(),
            };
            return View(employeePageVm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeePageVm employeePageVm)
        {
            if (!ModelState.IsValid)
            {
                employeePageVm.Departmants = _departmantManager.GetActives();
                employeePageVm.Positions = _positionManager.GetActives();
                employeePageVm.Branches = _branchManager.GetActives();
                return View(employeePageVm);
            }
            await _employeeManager.UpdateAsync(employeePageVm.appUsers.FirstOrDefault());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pasify(int id)
        {
            await _employeeManager.MakePassiveAsync(await _employeeManager.GetByIdAsync(id));
            return RedirectToAction("Index");
        }


    }
}
