using IK.ENTITIES.Models;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class BranchCreatePageVm
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        public int DepartmantId { get; set; }

        public List<Departmant> Departmants { get; set; }
    }
}
