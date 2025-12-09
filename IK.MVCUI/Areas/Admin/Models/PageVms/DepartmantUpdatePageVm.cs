using IK.ENTITIES.Models;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class DepartmantUpdatePageVm
    {
        public int Id { get; set; }
        public string DepartmantName { get; set; }
        public string Description { get; set; }

        public int PositionId { get; set; }
        public int BranchId { get; set; }

        public List<Position> Positions { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
