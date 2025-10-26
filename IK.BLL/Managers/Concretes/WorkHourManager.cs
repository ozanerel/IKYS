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
    public class WorkHourManager:BaseManager<WorkHour>,IWorkHourManager
    {
        readonly IWorkHourRepository _repository;
        public WorkHourManager(IWorkHourRepository repository):base(repository)
        {
            _repository = repository;
        }
    }
}
