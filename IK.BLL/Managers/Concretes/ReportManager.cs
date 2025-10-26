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
    public class ReportManager:BaseManager<Report>,IReportManager
    {
        readonly IReportRepository _repository;
        public ReportManager(IReportRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Report>> MontlhyReportsAsync(int month, int year)
        {
            var allReports = await _repository.GetAllAsync();
            return allReports.Where(r => r.CreatedDate.Month == month && r.CreatedDate.Year == year).ToList();
        }
    }
}
