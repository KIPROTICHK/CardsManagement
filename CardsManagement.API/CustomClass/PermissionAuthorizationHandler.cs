using Microsoft.AspNetCore.Authorization;

namespace CardsManagement.API.CustomClass
{
    //authorization handler that checks whether a user has the required permission, and if so, access is allowed
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private IHttpContextAccessor HttpContextAccessor { get; }
        private HttpContext HttpContext => HttpContextAccessor.HttpContext;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //var  re= HttpContext.Connection.RemoteIpAddress;
            // var token = await HttpContext.GetTokenAsync("access_token");


            if (context.User.HasClaim(x => x.Type == "ClientIPAddress" && !x.Value.StringCompare(HttpContext.Connection.RemoteIpAddress.ToString())))
            {
                context.Fail();
                await Task.CompletedTask;
            }
            foreach (var r in requirement.Permission)
            {
                if (context.User.HasClaim(x => x.Type == r.Type &&
                x.Value == r.Value))
                {
                    context.Succeed(requirement);
                }

            }
            await Task.CompletedTask;



        }


    }
}
