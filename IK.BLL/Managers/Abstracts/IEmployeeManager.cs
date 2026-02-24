using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IEmployeeManager:IManager<Employee>
    {
        Task ChangeDepartmentASync(int employeeId, int newDepartmantId);
        Task CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task UpdateSalaryAsync(int employeeId, decimal newSalary);
        Task HireFromJobApplication(JobApplication app, string userName, int departmanId, int branchId);

    }
}
