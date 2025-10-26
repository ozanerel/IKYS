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
    public class JobApplicationManager:BaseManager<JobApplication>, IJobApplicationManager
    {
        readonly IJobApplicationRepository _repository;
        public JobApplicationManager(IJobApplicationRepository repository):base(repository)
        {
            _repository = repository;
            
            
        }
        // Başvuru onayla
        public async Task ApproveApplicationAsync(int id)
        {
            var original = await _repository.GetByIdAsync(id);
            if (original != null)
            {
                var updated = original;
                updated.ApplicationStatus = ApplicationStatus.Approved;
                updated.Status = DataStatus.Approved;
                updated.UpdatedDate = DateTime.Now;
                await _repository.UpdateAsync(original,updated);
            }
        }

        // Başvuruyu reddet
        public async Task RejectApplicationAsync(int id)
        {
            var original = await _repository.GetByIdAsync(id);
            if (original != null)
            {
                var updated = original;
                updated.ApplicationStatus = ApplicationStatus.Rejected;
                updated.Status = DataStatus.Approved;
                updated.UpdatedDate = DateTime.Now;
                await _repository.UpdateAsync(original, updated);
            }
        }

        // Başvuru beklemeye al
        public async Task SetPendingAsync(int id)
        {
            var original = await _repository.GetByIdAsync(id);
            if (original != null)
            {
                var updated = original;
                updated.ApplicationStatus = ApplicationStatus.Pending;
                updated.Status = DataStatus.Approved;
                updated.UpdatedDate = DateTime.Now;
                await _repository.UpdateAsync(original, updated);
            }
        }
    }
}
