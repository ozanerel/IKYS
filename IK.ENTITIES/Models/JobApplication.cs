using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Enums;

namespace IK.ENTITIES.Models
{
    public class JobApplication:BaseEntity
    {
        //İş Başvurusu
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ApplicateDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public string CVFilePath { get; set; }


        public int PositionId { get; set; }


        //Relational Properties
        public virtual Position Position { get; set; }

    }
}
