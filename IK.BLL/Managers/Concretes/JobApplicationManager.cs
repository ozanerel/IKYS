using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return;

            app.ApplicationStatus = ApplicationStatus.Approved;
            app.Status = DataStatus.Approved;
            await _repository.UpdateAsync(app, app);
        }

        public async Task<List<JobApplication>> GetApplicationsByPositionAsync(int positionId)
        {
            return await _repository
                .Where(x => x.PositionId == positionId)
                .Include(x => x.Position)
                .ToListAsync();
        }

        // Başvuruyu reddet
        public async Task RejectApplicationAsync(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return;

            app.ApplicationStatus = ApplicationStatus.Rejected;
            app.Status = DataStatus.Passive;
            await _repository.UpdateAsync(app, app);
        }

        // Başvuru beklemeye al
        public async Task SetPendingAsync(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return;

            app.ApplicationStatus = ApplicationStatus.Pending;
            app.Status = DataStatus.Pending;
            await _repository.UpdateAsync(app, app);
        }

        public async Task UploadCvAsync(int id, string filePath)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null)
                throw new Exception("Başvuru bulunamadı.");

            if (string.IsNullOrWhiteSpace(filePath))
                throw new Exception("CV dosya yolu geçersiz.");

            app.CVFilePath = filePath;
            app.Status = DataStatus.Updated;
            app.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(app, app);
        }
    }
}
