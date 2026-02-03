using System.ComponentModel.DataAnnotations;
using IK.ENTITIES.Enums;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class HireEmployeePageVm
    {
        public int JobApplicationId { get; set; }

        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // ADMIN DOLDURUR
        [Required]
        public string TCKN { get; set; }

        [Required]
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }

        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public JobType JobType { get; set; }

        public int PositionId { get; set; }

        [Required]
        public int DepartmanId { get; set; }
        [Required]
        public int BranchId { get; set; }
    }
}
