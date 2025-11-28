using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Models;
using IK.MVCUI.Areas.Admin.Models.PageVms;
using Microsoft.AspNetCore.Identity;
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
        readonly IAppUserManager _appUserManager;
        public EmployeeController(IEmployeeManager employeeManager, IDepartmantManager departmantManager, IPositionManager positionManager, IBranchManager branchManager,IAppUserManager appUserManager)
        {
            _employeeManager = employeeManager;
            _departmantManager = departmantManager;
            _positionManager = positionManager;
            _branchManager = branchManager;
            _appUserManager = appUserManager;
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

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreatePageVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departmants = _departmantManager.GetActives().ToList();
                vm.Positions = _positionManager.GetActives().ToList();
                vm.Branches = _branchManager.GetActives().ToList();
                //return View(vm);
            }

            // 1) Identity Kullanıcı oluşturma
            var appUser = await _appUserManager.CreateUserWithRoleAsync(
                vm.UserName,          // username & email
                vm.Email,
                vm.Password,       // admin tarafından girilen şifre
                "Employee"         // rol
            );

            //if (appUser == null)
            //{
            //    ModelState.AddModelError("", "Kullanıcı oluşturulamadı!");
            //    vm.Departmants = _departmantManager.GetActives().ToList();
            //    vm.Positions = _positionManager.GetActives().ToList();
            //    vm.Branches = _branchManager.GetActives().ToList();
            //    //return View(vm);
            //}

            // 2) Employee oluşturma
            var employee = new IK.ENTITIES.Models.Employee
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
                PositionId = vm.PositionId,
                AppUserId = appUser.Id   // ilişkiyi kurduk
            };

            // 3) Employee kaydetme
            await _employeeManager.CreateAsync(employee);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeManager.GetByIdAsync(id);

            var vm = new EmployeeUpdatePageVm
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Salary = employee.Salary,
                DepartmanId = employee.DepartmanId,
                PositionId = employee.PositionId,
                BranchId = employee.BranchId,
                Departmants = _departmantManager.GetActives(),
                Positions = _positionManager.GetActives(),
                Branches = _branchManager.GetActives()
            };

            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdatePageVm vm)
        {
            Console.WriteLine("POST AD: " + vm.FirstName);
            Console.WriteLine("POST ID: " + vm.Id);

            if (!ModelState.IsValid)
            {
                vm.Departmants = _departmantManager.GetActives();
                vm.Positions = _positionManager.GetActives();
                vm.Branches = _branchManager.GetActives();
                
            }

            var employee = await _employeeManager.GetByIdAsync(vm.Id);
            if (employee == null) return NotFound();

            employee.FirstName = vm.FirstName;
            employee.LastName = vm.LastName;
            employee.Email = vm.Email;
            employee.PhoneNumber = vm.PhoneNumber;
            employee.Salary = vm.Salary;

            employee.DepartmanId = vm.DepartmanId;
            employee.PositionId = vm.PositionId;
            employee.BranchId = vm.BranchId;

            await _employeeManager.UpdateAsync(employee);

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Pasify(int id)
        {
            await _employeeManager.MakePassiveAsync(await _employeeManager.GetByIdAsync(id));
            return RedirectToAction("Index");
        }


    }
}
