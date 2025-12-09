using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class BranchCreatePageVm
    {
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        public int DepartmantId { get; set; }

        [BindNever]
        [ValidateNever]//required zorunluluğu çalışmasın diye bu attribute'ları kullandık
        public List<Departmant> Departmants { get; set; }
    }
}
