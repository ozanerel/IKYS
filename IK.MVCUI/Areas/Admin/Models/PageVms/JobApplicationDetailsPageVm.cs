using IK.ENTITIES.Models;

namespace IK.MVCUI.Areas.Admin.Models.PageVms
{
    public class JobApplicationDetailsPageVm
    {
        //Gösterim için
        public JobApplication JobApplication { get; set; }

        //Admin formu için
        public HireEmployeePageVm HireVm { get; set; }
    }
}
