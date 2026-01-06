using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using IK.MVCUI.Areas.Admin.Models.PageVms;
using Microsoft.AspNetCore.Mvc;

namespace IK.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PayrollController : Controller
    {
        private readonly IPayrollManager _payrollManager;
        private readonly IEmployeeManager _employeeManager;

        public PayrollController(IPayrollManager payrollManager, IEmployeeManager employeeManager)
        {
            _payrollManager = payrollManager;
            _employeeManager = employeeManager;
        }

        public async Task<IActionResult> Index()
        {
           var payrolls = await _payrollManager.GetPayrollsWithEmployeeAsync();

            return View(payrolls);
        }


        // Maaş oluşturma sayfası
      
        public async Task<IActionResult> Create()
        {
            var vm = new PayrollCreatePageVm
            {
                Employees = _employeeManager.GetActives().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PayrollCreatePageVm pvm)
        {
            ModelState.Remove("Employees");
            if (!ModelState.IsValid)
            {
                pvm.Employees = _employeeManager.GetActives().ToList();
                return View(pvm);
            }

            await _payrollManager.GeneratePayrollAsync(
                pvm.EmployeeId,
                pvm.Period,
                pvm.HourlyRate,
                pvm.TaxRate,
                pvm.Bonuses
            );



            TempData["Success"] = "Bordro başarıyla oluşturuldu.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await _payrollManager.CancelPayrollAsync(id);
            return RedirectToAction("Index");
        }
    }
}
