using IK.ENTITIES.Models;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class EmployeeUpdatePageVm
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }

        //Dropdownlar
        public int DepartmanId { get; set; }
        public int PositionId { get; set; }
        public int BranchId { get; set; }

        public List<Departmant> Departmants { get; set; }
        public List<Position> Positions { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
