using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Services.Abstracts
{
    public interface IEmployeeHireService
    {
        Task HireFromJobApplication(JobApplication app,string userName, int departmanId, int branchId);
       
    }
}
