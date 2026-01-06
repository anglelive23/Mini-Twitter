using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Mini_Twitter.Infrastructure.Authorization.Helpers
{
    public static class AuthorizationHelper
    {
        public static bool TryGetUserIdFromAuthorizationContext(AuthorizationHandlerContext context, out string userId)
        {
            userId = string.Empty;

            var uidClaim = context.User.Claims.FirstOrDefault(c => c.Type == "uid");
            if (uidClaim is null)
                return false;

            userId = uidClaim.Value;
            return true;
        }

        public static bool TryGetRoleFromAuthorizationContext(AuthorizationHandlerContext context, out string role)
        {
            role = context.User.FindFirstValue(ClaimTypes.Role)!;
            return string.IsNullOrEmpty(role) ? false : true;
        }
    }
}
