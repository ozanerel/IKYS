namespace IK.MVCUI.Areas.Employee.Models.PageVms
{
    public class EmployeeDashboardPageVm
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BranchName { get; set; }
        public string DepartmanName { get; set; }
        public DateTime StartDate { get; set; }

        // Chart verileri
        public List<string> ChartLabels { get; set; } = new();
        public List<decimal> ChartData { get; set; } = new();
    }
}
