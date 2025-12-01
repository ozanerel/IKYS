using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using IK.MVCUI.Areas.Admin.Models.PageVms;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchManager _branchManager;
        private readonly IDepartmantManager _departmantManager;

        public BranchController(IBranchManager branchManager, IDepartmantManager departmantManager)
        {
            _branchManager = branchManager;
            _departmantManager = departmantManager;
        }


        public async Task<IActionResult> Index()
        {
            List<Branch> branches = await _branchManager.GetAllAsync();
            return View(branches);
        }

        public IActionResult Create()
        {
            var vm = new BranchCreatePageVm
            {
                Departmants = _departmantManager.GetActives().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BranchCreatePageVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departmants = _departmantManager.GetActives().ToList();
                return View(vm);
            }

            var branch = new Branch
            {
                BranchName = vm.BranchName,
                City = vm.City,
                Address = vm.Address
            };

            // 1) Branch kaydedilir
            await _branchManager.CreateAsync(branch);

            // 2) Eğer departman seçilmişse branch'a bağlanır
            if (vm.DepartmantId != 0)
            {
                await _branchManager.AddDepartmantToBranchAsync(branch.Id, vm.DepartmantId);
            }

            return RedirectToAction("Index");
        }

    }
}
