using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IWorkHourManager : IManager<WorkHour>
    {

        // Çalışanın bir gün için giriş-çıkış saatlerini kaydeder.
        Task AddWorkHoursAsync(int employeeId, DateTime entryTime, DateTime exitTime);


        // Çalışma saatini (örneğin mesai kaydını) onaylar.
        Task ApproveWorkHourAsync(int id);


        // Belirli bir çalışanın toplam çalışma süresini hesaplar.
        Task<decimal> GetTotalWorkHoursByEmployeeAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
