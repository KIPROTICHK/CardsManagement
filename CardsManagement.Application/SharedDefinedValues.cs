using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagement.Application
{
    public class SharedDefinedValues
    {
        
        public const string DefaultConnection = nameof(DefaultConnection);

        public   const int TokenHourlyDuration = 12;

        public class CardStatuses
        {
            public const string ToDo = "To Do";
            public const string InProgress = "In Progress";
            public const string Done = "Done";
        }
        public class UserType
        {
            public const string Admin = nameof(Admin); 
            public const string Member = nameof(Member);

        }

        public class Token
        {
            public class Statuses
            {
                public const string Optional = "Access Token optional";
            }

            public class Validity
            {
                public const int Hours = 12;
            }
        }

        public class Permissions
        {
             public const string Admin = nameof(Admin);
             public const string Member = nameof(Member); 
 
        }

        public static class PermissionType
        {
            public const string role = ClaimTypes.Role;
        }


        public class IdentityData
        {
            public class Roles
            {
                 public const string Admin = nameof(Admin);
                public const string Member = nameof(Member); 
            }

            public static List<CustomRole> GetCustomRoles()
            {
                return new List<CustomRole>()
                {
                    
                    new CustomRole
                    {
                        Name=Roles.Admin,
                        ApplicableToAdmin=true,
                        Description="Admin"

                    },
                    new CustomRole
                    {
                        Name=Roles.Member,
                        ApplicableToAdmin=true,
                        ApplicableToMember=true,
                        Description="public user"

                    },
                     

                };
            }
        }
        public class CustomRole
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool ApplicableToAdmin { get; set; }
             public bool ApplicableToMember { get; set; }

        }
    }
}
