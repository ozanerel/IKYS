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
    public class PositionManager:BaseManager<Position>, IPositionManager
    {
        readonly IPositionRepository _repository;
        readonly IEmployeeRepository _employeeRepository;
        public PositionManager(IPositionRepository repository,IEmployeeRepository employeeRepository):base(repository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
        }

        public async Task AssignEmployeeToPositionAsync(int employeeId, int positionId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return;

            employee.PositionId = positionId;
            employee.UpdatedDate = DateTime.Now;
            await _employeeRepository.UpdateAsync(employee, employee);

        }
    }
}
