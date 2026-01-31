namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class JobApplicationCreatePageVm
    {
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int PositionId { get; set; }
        public bool PrivacyAccepted { get; set; }
        public IFormFile CvFile { get; set; }
    }
}
