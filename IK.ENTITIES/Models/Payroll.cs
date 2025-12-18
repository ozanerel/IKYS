using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class Payroll : BaseEntity
    {

        private decimal _grossSalary;
        private decimal _netSalary;
        private decimal _deductions;

        private decimal _bonuses;
        private string _period;
        private decimal _totalHours;
        private decimal _hourlyRate;
        private decimal _taxRate;


        //Bordro:Çalışan maaş detaylarını içerir
        //Örnek "2024-06"
        public string Period
        {
            get { return _period; }
            set
            {
                _period = value;
            }
        }

        //Çalışanın toplam saatleri WorkHourManager'dan gelir
        public decimal TotalHours
        {
            get { return _totalHours; }
            set
            {
                _totalHours = value;
                CalculatePayroll();
            }
        }

        //Saatlik ücret dışarıdan girilir
        public decimal HourlyRate 
        {
            get { return _hourlyRate; }
            set
            {
                _hourlyRate = value;
                CalculatePayroll();
            } 
        }

        public decimal TaxRate 
        {
            get
            {
                return _taxRate;
            }
            set
            {
                _taxRate = value;
                CalculatePayroll();
            }
        }

        public decimal Bonuses
        {
            get
            {
                return _bonuses;
            }
            set
            {
                _bonuses = value;
                CalculatePayroll();
            }
        }

        //Dışarıdan set alamayan propertyler
        //Brüt Maaş
        public decimal GrossSalary 
        { 
            get{ return _grossSalary; } 
        }
        public decimal NetSalary 
        {
            get { return _netSalary; }  
        }
       
        //Kesintiler
        public decimal Deductions 
        {
            get { return _deductions; }  
        }



        private void CalculatePayroll()
        {
            if (_totalHours <= 0 || _hourlyRate <= 0 || _taxRate < 0)
                return;

            _grossSalary = (_totalHours * _hourlyRate) + _bonuses;
            _deductions = _grossSalary * _taxRate;
            _netSalary = _grossSalary - _deductions;
        }


        public int EmployeeId { get; set; }

        //Relational Properties
        public virtual Employee Employee { get; set; }

    }
}
