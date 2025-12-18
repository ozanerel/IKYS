using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IK.ENTITIES.Models
{
    public class WorkHour:BaseEntity 
    {

        private DateTime _entryTime { get; set; }
        private DateTime _exitTime { get; set; }
        private decimal _totalHours { get; set; }
        private decimal _normalHours { get; set; }
        private decimal _overtimeHours { get; set; }
        //Mesai
        public DateTime EntryTime 
        {
            get 
            {
                return _entryTime;
            }
            set 
            {
                _entryTime = value;
                CalculateHour();
            } 
        }
        public DateTime ExitTime 
        {
            get
            {
                return _exitTime;
            }
            set
            {
                _exitTime = value;
                CalculateHour();
            }
        }
        public decimal TotalHours 
        {
            get { return _totalHours; }
        }

        //Olması gereken mesai saati
        public decimal NormalHours 
        { 
            get { return _normalHours; }
        }

        //Yapılan ek mesai saatleri
        public decimal OvertimeHours 
        { 
            get{ return _overtimeHours; } 
        }


        private void CalculateHour()
        {
            if (_entryTime == default || _exitTime == default)
                return;

            if (_exitTime <= _entryTime)
            {
                _totalHours = 0;
                _normalHours = 0;
                _overtimeHours = 0;
                return;
            }

            _totalHours = (decimal)(_exitTime - _entryTime).TotalHours;

            if (TotalHours <= 8)
            {
                _normalHours = TotalHours;
                _overtimeHours = 0;
            }
            else
            {
                _normalHours = 8;
                _overtimeHours = TotalHours - 8;
            }
        }

        public int EmployeeId { get; set; }

        //Relational Properties
        public virtual Employee Employee { get; set; }
    }
}
