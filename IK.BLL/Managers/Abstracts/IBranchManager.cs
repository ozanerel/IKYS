using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IBranchManager:IManager<Branch>
    {
        Task ActiveBranchAsync(int id);
        Task AssignEmployeeToBranchAsync(int employeeId,int branchId);
        Task<List<Employee>> GetEmployeesByBranchAsync(int branchId);
        Task AddDepartmantToBranchAsync(int branchId, Departmant departmant);
    }
}
