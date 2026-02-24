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
using QuestPDF.Fluent;

namespace IK.BLL.Managers.Concretes
{
    public class PayrollManager : BaseManager<Payroll>, IPayrollManager
    {
        readonly IPayrollRepository _repository;
        readonly IWorkHourRepository _workHourRepository;
        readonly IEmployeeRepository _employeeRepository;
        public PayrollManager(IPayrollRepository repository, IWorkHourRepository workHourRepository, IEmployeeRepository employeeRepository) : base(repository)
        {
            _repository = repository;
            _workHourRepository = workHourRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task CancelPayrollAsync(int id)
        {
            var payroll = await _repository.GetByIdAsync(id);
            if (payroll == null)
                throw new Exception("Bordro bulunamadı.");

            payroll.Status = DataStatus.Deleted;
            payroll.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(payroll,payroll);


        }

        public async Task GeneratePayrollAsync(int employeeId, string period, decimal hourlyRate, decimal taxRate, decimal bonuses = 0)
        {
            bool payrollExists = await _repository.Where(p =>
                    p.EmployeeId == employeeId &&
                    p.Period == period &&
                    p.Status != DataStatus.Deleted
                ).AnyAsync();

            if (payrollExists)
                throw new Exception("Bu çalışan için bu döneme ait bordro zaten oluşturulmuş.");

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
                TotalHours = totalHours,
                HourlyRate = hourlyRate,
                TaxRate = taxRate,
                //GrossSalary = Math.Round(grossSalary, 2),
                //NetSalary = Math.Round(netSalary, 2),
                Bonuses = Math.Round(bonuses, 2),
                //Deductions = Math.Round(deductions, 2),
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _repository.CreateAsync(payroll);
        }

        public byte[] GeneratePayrollPdf(Payroll payroll)
        {
            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text("MAAŞ BORDROSU")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Çalışan: {payroll.Employee.FirstName} {payroll.Employee.LastName}");
                        column.Item().Text($"Dönem: {payroll.Period}");
                        column.Item().Text($"Toplam Saat: {payroll.TotalHours}");
                        column.Item().Text($"Brüt Maaş: {payroll.GrossSalary:C}");
                        column.Item().Text($"Kesinti: {payroll.Deductions:C}");
                        column.Item().Text($"Net Maaş: {payroll.NetSalary:C}")
                            .Bold();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Oluşturulma Tarihi: {DateTime.Now:dd.MM.yyyy}");
                });
            });

            return document.GeneratePdf();
        }

        public async Task<List<Payroll>> GetPayrollsByEmployeeIdAsync(int employeeId)
        {
            return await _repository.Where(p => p.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<Payroll>> GetPayrollsByPeriodAsync(string period)
        {
            //Belirli bir döneme ait bordro kayıtlarını getirir.
            return await _repository.Where(p => p.Period == period).ToListAsync();
        }

        public async Task<List<Payroll>> GetPayrollsWithEmployeeAsync()
        {
            return await _repository.GetPayrollsWithEmployeeAsync();
        }
    }
}
