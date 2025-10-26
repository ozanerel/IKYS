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
    public class PayrollManager:BaseManager<Payroll>,IPayrollManager
    {
        readonly IPayrollRepository _repository;
        readonly IWorkHourRepository _workHourRepository;
        readonly IEmployeeRepository _employeeRepository;
        public PayrollManager(IPayrollRepository repository, IWorkHourRepository workHourRepository,IEmployeeRepository employeeRepository):base(repository)
        {
            _repository = repository;
            _workHourRepository = workHourRepository;
            _employeeRepository = employeeRepository;
        }

       

        public async Task GeneratePayrollAsync(int employeeId, string period, decimal hourlyRate, decimal taxRate, decimal bonuses = 0)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return;

            // Dönemin tarih aralığını belirleyelim (örn. "2024-06")
            var periodParts = period.Split('-');
            int year = int.Parse(periodParts[0]);
            int month = int.Parse(periodParts[1]);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Çalışma saatlerini al
            var workHours = await _workHourRepository
                .Where(w => w.EmployeeId == employeeId && w.EntryTime >= startDate && w.ExitTime <= endDate)
                .ToListAsync();

            if (workHours == null || !workHours.Any())
                throw new Exception("Bu dönem için çalışma kaydı bulunamadı.");

            // Toplam çalışma saatini hesapla
            decimal totalHours = workHours.Sum(w => w.TotalHours);

            // Maaş hesaplaması
            decimal grossSalary = totalHours * hourlyRate + bonuses;
            decimal deductions = grossSalary * taxRate;
            decimal netSalary = grossSalary - deductions;

            var payroll = new Payroll
            {
                EmployeeId = employeeId,
                Period = period,
                GrossSalary = Math.Round(grossSalary, 2),
                NetSalary = Math.Round(netSalary, 2),
                Bonuses = Math.Round(bonuses, 2),
                Deductions = Math.Round(deductions, 2),
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _repository.CreateAsync(payroll);
        }

        public async Task<List<Payroll>> GetPayrollsByPeriodAsync(string period)
        {
            //Belirli bir döneme ait bordro kayıtlarını getirir.
            return await _repository.Where(p => p.Period == period).ToListAsync();
        }
    }
}
