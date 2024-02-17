using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CardsManagement.Domain
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
        public bool ForAdminUser { get; set; }
        public bool ApplicableToMember { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }

    }
}
