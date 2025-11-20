using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Concretes
{
    public class EmployeeManager:BaseManager<Employee>,IEmployeeManager
    {
        readonly IEmployeeRepository _repository;
        public EmployeeManager(IEmployeeRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task ChangeDepartmentASync(int employeeId, int newDepartmantId)
        {
            var employee =await _repository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.DepartmanId = newDepartmantId;
            await _repository.UpdateAsync(employee, employee);
        }

        public Task CreateAsync(AppUser? appUser)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser? appUser)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSalaryAsync(int employeeId, decimal newSalary)
        {
            var employee = await _repository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.Salary = newSalary;
            await _repository.UpdateAsync(employee,employee);
        }
    }
}
