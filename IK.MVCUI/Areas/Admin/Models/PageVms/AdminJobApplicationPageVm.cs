using IK.ENTITIES.Enums;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class AdminJobApplicationPageVm
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
