using Microsoft.AspNetCore.Authorization;

namespace CardsManagement.API.CustomClass
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public List<PermissionItem> Permission { get; private set; }

        public PermissionRequirement(List<PermissionItem> permission)
        {
            Permission = permission;
        }

    }

    public class PermissionItem
    {
        public string Type { get; set; }
        public string Value { get; set; }

    }
}
