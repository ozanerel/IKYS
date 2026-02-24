using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IPayrollManager:IManager<Payroll>
    {
        //Çalışanın belirli bir dönem için maaşını hesaplayıp bordro oluşturur.
        Task GeneratePayrollAsync(int employeeId,string period ,decimal hourlyRate,decimal taxRate,decimal bonuses = 0);

        //Belirli bir döneme ait bordro kayıtlarını getirir.
        Task<List<Payroll>> GetPayrollsByPeriodAsync(string period);

        Task<List<Payroll>> GetPayrollsByEmployeeIdAsync(int employeeId);

        Task<List<Payroll>> GetPayrollsWithEmployeeAsync();


        Task CancelPayrollAsync(int id);

        byte[] GeneratePayrollPdf(Payroll payroll);


    }
}
