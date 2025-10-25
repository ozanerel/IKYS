using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Enums;

namespace IK.ENTITIES.Models
{
    public class Employee : BaseEntity
    {
        //Çalışan
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCKN { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Salary { get; set; }
        public JobType JobType { get; set; }

        public int? AppUserId { get; set; }
        public int DepartmanId { get; set; }
        public int PositionId { get; set; }
        public int BranchId { get; set; }

        //Relational Properties
        public virtual AppUser AppUser { get; set; }
        public virtual Departmant Departmant{ get; set; }
        public virtual Position Position { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
        public virtual ICollection<WorkHour> WorkHours { get; set; }
        public virtual ICollection<CareerPlan> CareerPlans { get; set; }
        public virtual EmployeeQualification EmployeeQualification { get; set; }

    }
}
