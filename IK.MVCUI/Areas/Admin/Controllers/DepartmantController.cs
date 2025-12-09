using Bogus.Bson;
using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using IK.MVCUI.Areas.Admin.Models.PageVms;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmantController : Controller
    {
        private readonly IDepartmantManager _departmantManager;
        private readonly IBranchManager _branchManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IPositionManager _positionManager;
        public DepartmantController(IDepartmantManager departmantManager, IBranchManager branchManager, IEmployeeManager employeeManager, IPositionManager positionManager)
        {
            _departmantManager = departmantManager;
            _branchManager = branchManager;
            _employeeManager = employeeManager;
            _positionManager = positionManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Departmant> departmants = await _departmantManager.GetAllAsync(); 
            return View(departmants);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new DepartmantCreatePageVm()
            {
                Positions = _positionManager.GetActives().ToList(),
                Branches = _branchManager.GetActives().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmantCreatePageVm pvm)
        {
            ModelState.Remove("Positions");
            ModelState.Remove("Branches");

            if (!ModelState.IsValid)
            {
                pvm.Positions = _positionManager.GetActives().ToList();
                pvm.Branches = _branchManager.GetActives().ToList();
                return View(pvm);
            }

            var departmant = new Departmant
            {
                DepartmantName = pvm.DepartmantName,
                Description = pvm.Description,
                BranchId = pvm.BranchId
            };

            await _departmantManager.CreateAsync(departmant);

            // Pozisyon seçilmişse departmana bağla
            if (pvm.PositionId > 0)
            {
                await _departmantManager.GetPositionsInDepartmantAsync(departmant.Id);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var departmant = await _departmantManager.GetByIdAsync(id);

            var vm = new DepartmantUpdatePageVm
            {
                Id = departmant.Id,
                DepartmantName = departmant.DepartmantName,
                Description = departmant.Description,
                Positions = _positionManager.GetActives().ToList(),
                Branches = _branchManager.GetActives().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmantUpdatePageVm pageVm)
        {
            var departmant = await _departmantManager.GetByIdAsync(pageVm.Id);
            if (departmant == null) return NotFound();

            departmant.Id = pageVm.Id;
            departmant.DepartmantName = pageVm.DepartmantName;
            departmant.Description = pageVm.Description;
           
            await _departmantManager.UpdateAsync(departmant);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pacify(int id)
        {
            await _departmantManager.MakePassiveAsync(await _departmantManager.GetByIdAsync(id));
            return RedirectToAction("Index");
        }

    }
}
