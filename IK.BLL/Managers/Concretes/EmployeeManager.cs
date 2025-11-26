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
            await _repository.UpdateAsync(employee);
        }

        public async Task CreateAsync(Employee employee)
        {
            await _repository.CreateAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            //var original = await _repository.GetByIdAsync(employee.Id);
            //if (original != null)
            //{
            //    await _repository.UpdateAsync(original, employee);
            //}
            await _repository.UpdateAsync(employee);
        }

        public async Task UpdateSalaryAsync(int employeeId, decimal newSalary)
        {
            var employee = await _repository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.Salary = newSalary;
            await _repository.UpdateAsync(employee);
        }
    }
}
