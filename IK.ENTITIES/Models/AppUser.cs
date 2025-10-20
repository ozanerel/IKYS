using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IK.ENTITIES.Models
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        public Guid ActivationCode { get; set; } //Her kullanıcım icin sadece ona özgü unique bir kod olusturmak isterim ki onun Email'ine o kodu gönderebileyim...Böylece ben onun Email'ine gidip oradaki linke tıkladıgını o kodu bana geri ulastırdıgında anlarım...

        public DateTime CreatedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
        public DateTime? DeletedDate { get ; set ; }
        public DataStatus Status { get; set; }

        //Relational Properties
        public AppUserProfile AppUserProfile { get; set; }
        public virtual Employee Employee { get; set; }


    }
}
