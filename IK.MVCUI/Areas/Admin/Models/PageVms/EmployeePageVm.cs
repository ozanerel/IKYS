using IK.ENTITIES.Models;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class EmployeePageVm
    {
        public List<Departmant> Departmants { get; set; }
        public List<Position> Positions { get; set; }
        public List<Branch> Branches { get; set; }

        public List<AppUser> appUsers  { get; set; }
    }
}
