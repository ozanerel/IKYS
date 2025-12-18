namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class HomePageVm
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public string BranchName { get; set; }
        //public DateTime BirthDate { get; set; }
        public DateTime StartedDate { get; set; }
        public decimal Salary { get; set; }
    }
}
