using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IDepartmantManager:IManager<Departmant>
    {
        Task AddEmployeeToDepartmantAsync(int employeeId, int departmantId);
        Task<List<Position>> GetPositionsInDepartmantAsync(int departmantId);
    }
}
