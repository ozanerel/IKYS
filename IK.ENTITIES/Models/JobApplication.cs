using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IK.ENTITIES.Models
{
    public class JobApplication:BaseEntity
    {
        //İş Başvurusu
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? TCKN { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Salary { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public JobType? JobType { get; set; }
        public DateTime ApplicateDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public string? CVFilePath { get; set; }
        public bool? PrivacyAccepted { get; set; }


        public int PositionId { get; set; }


        //Relational Properties
        [ValidateNever]
        public virtual Position Position { get; set; }

    }
}
