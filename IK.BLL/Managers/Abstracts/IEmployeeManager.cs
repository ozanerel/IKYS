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
        Task CreateAsync(AppUser? appUser);
        Task UpdateAsync(AppUser? appUser);
        Task UpdateSalaryAsync(int employeeId, decimal newSalary);
    }
}
