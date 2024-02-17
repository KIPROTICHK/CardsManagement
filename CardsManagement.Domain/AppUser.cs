using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CardsManagement.Domain
{

    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }
        public string ApprovedBy { get; set; }
        public bool Approved { get; set; }
        public string OTP { get; set; }
        public DateTime? OTPDate { get; set; }
        public int? OTPEndMin { get; set; }
        public int? OTPCounter { get; set; }
        public string UserType { get; set; }
        public string ProfileImage { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; } 
    }

}
