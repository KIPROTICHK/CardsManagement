using Microsoft.AspNetCore.Identity;
using System;

namespace CardsManagement.Domain
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public virtual AppUser UserNavigation { get; set; }
        public virtual AppRole RoleNavigation { get; set; }
    }
}
