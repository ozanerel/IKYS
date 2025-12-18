using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class PayrollCreatePageVm
    {
        public string Period { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Bonuses { get; set; }

        public int EmployeeId { get; set; }

        [BindNever]
        [ValidateNever]
        public List<IK.ENTITIES.Models.Employee> Employees { get; set; }
    }
}
