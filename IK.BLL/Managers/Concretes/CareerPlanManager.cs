using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Concretes
{
    public class CareerPlanManager:BaseManager<CareerPlan>,ICareerPlanManager
    {
        readonly ICareerPlanRepository _repository;
        public CareerPlanManager(ICareerPlanRepository repository):base(repository)
        {
            _repository = repository;
            
            
        }

        public async Task ApproveCareerPlanAsync(int id)
        {
            //Kariyeri planını onaylar.
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return;

            plan.Status = DataStatus.Approved;
            plan.UpdatedDate = DateTime.Now;
            await _repository.UpdateAsync(plan,plan);
        }
    }
}
