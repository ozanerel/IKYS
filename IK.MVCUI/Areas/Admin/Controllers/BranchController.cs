using IK.BLL.Managers.Abstracts;
using IK.BLL.Managers.Concretes;
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
            ModelState.Remove("Departmants");//Eski modelstate'i bulunduruyorsa diye post sırasında tamamen siliyoruz.

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

            // Önce şubeyi kaydet
            await _branchManager.CreateAsync(branch);

            // Departman seçilmişse şubeye bağla
            if (vm.DepartmantId > 0)
            {
                await _branchManager.AddDepartmantToBranchAsync(branch.Id, vm.DepartmantId);
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int id)
        {
            var branch = await _branchManager.GetByIdAsync(id);

            var vm = new BranchUpdatePageVm
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                City = branch.City,
                Address = branch.Address,
                
                //DepartmantId = branch.DepartmantId,
                
                Departmants = _departmantManager.GetActives(),
               
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Update(BranchUpdatePageVm pvm)
        {
            var branch = await _branchManager.GetByIdAsync(pvm.Id);
            if (branch == null) return NotFound();

            branch.Id = pvm.Id;
            branch.BranchName = pvm.BranchName;
            branch.City = pvm.City;
            branch.Address = pvm.Address;
            
            await _branchManager.UpdateAsync(branch);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Pasify(int id)
        {
            await _branchManager.MakePassiveAsync(await _branchManager.GetByIdAsync(id));
            return RedirectToAction("Index");
        }

    }
}
