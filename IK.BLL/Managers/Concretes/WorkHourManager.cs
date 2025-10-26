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

namespace IK.BLL.Managers.Concretes
{
    public class WorkHourManager : BaseManager<WorkHour>, IWorkHourManager
    {
        readonly IWorkHourRepository _repository;
        public WorkHourManager(IWorkHourRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task AddWorkHoursAsync(int employeeId, DateTime entryTime, DateTime exitTime)
        {
            if (exitTime <= entryTime)
                throw new ArgumentException("Çıkış saati giriş saatinden önce olamaz.");

            var totalHours = (decimal)(exitTime - entryTime).TotalHours;

            var workHour = new WorkHour
            {
                EmployeeId = employeeId,
                EntryTime = entryTime,
                ExitTime = exitTime,
                TotalHours = totalHours,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _repository.CreateAsync(workHour);
        }

        public async Task ApproveWorkHourAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
                throw new Exception("Kayıt bulunamadı.");

            record.Status = DataStatus.Approved;
            record.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(record, record);
        }

        public async Task<decimal> GetTotalWorkHoursByEmployeeAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _repository.Where(w => w.EmployeeId == employeeId);

            if (startDate.HasValue)
                query = query.Where(w => w.EntryTime >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(w => w.ExitTime <= endDate.Value);

            var totalHours = await query.SumAsync(w => w.TotalHours);

            return totalHours;
        }
    }
}
