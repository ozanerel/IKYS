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
            var vm = new EmployeeCreatePageVm
            {
                Departmants = _departmantManager.GetActives().ToList(),
                Positions = _positionManager.GetActives().ToList(),
                Branches = _branchManager.GetActives().ToList(),
                StartDate = DateTime.Now
            };

            return View(vm);
        }

        // Create POST
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreatePageVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departmants = _departmantManager.GetActives().ToList();
                vm.Positions = _positionManager.GetActives().ToList();
                vm.Branches = _branchManager.GetActives().ToList();

                return View(vm);
            }

            IK.ENTITIES.Models.Employee employee = new IK.ENTITIES.Models.Employee
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                TCKN = vm.TCKN,
                BirthDate = vm.BirthDate,
                Salary = vm.Salary,
                StartDate = vm.StartDate,
                MaritalStatus = vm.MaritalStatus,
                Gender = vm.Gender,
                JobType = vm.JobType,
                Address = vm.Address,
                BranchId = vm.BranchId,
                DepartmanId = vm.DepartmanId,
                PositionId = vm.PositionId
            };

            await _employeeManager.CreateAsync(employee);
            return RedirectToAction("Index");
        }

        // Update GET
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeManager.GetByIdAsync(id);

            if (employee == null) return NotFound();

            var vm = new EmployeeUpdatePageVm
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                DepartmanId = employee.DepartmanId,
                PositionId = employee.PositionId,
                BranchId = employee.BranchId,
                Departmants = _departmantManager.GetActives().ToList(),
                Positions = _positionManager.GetActives().ToList(),
                Branches = _branchManager.GetActives().ToList()
            };

            return View(vm);
        }



        // Update POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(IK.ENTITIES.Models.Employee employee)
        {
            ModelState.Remove("Departmant");
            ModelState.Remove("Position");
            ModelState.Remove("Branch");
            ModelState.Remove("AppUser");

            if (!ModelState.IsValid)
            {
                ViewBag.Departmants = _departmantManager.GetActives();
                ViewBag.Positions = _positionManager.GetActives();
                ViewBag.Branches = _branchManager.GetActives();
                return View(employee);
            }

            var original = await _employeeManager.GetByIdAsync(employee.Id);
            if (original == null) return NotFound();

            original.FirstName = employee.FirstName;
            original.LastName = employee.LastName;
            original.Email = employee.Email;
            original.PhoneNumber = employee.PhoneNumber;
            original.Salary = employee.Salary;
            original.DepartmanId = employee.DepartmanId;
            original.PositionId = employee.PositionId;
            original.BranchId = employee.BranchId;

            await _employeeManager.UpdateAsync(original);
            return RedirectToAction("Index");
        }






        public async Task<IActionResult> Pasify(int id)
        {
            await _employeeManager.MakePassiveAsync(await _employeeManager.GetByIdAsync(id));
            return RedirectToAction("Index");
        }


    }
}
