using Microsoft.AspNetCore.Authorization;
using Mini_Twitter.Application.Authorization.Requirements;
using Mini_Twitter.Infrastructure.Authorization.Helpers;

namespace Mini_Twitter.Infrastructure.Authorization.Handlers
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly TwitterContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PermissionAuthorizationHandler(TwitterContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var requiredPermission = $"{requirement.Module}.{requirement.Permission}";

            // validate from the JWT itself first
            //var permissionClaims = context.User.Claims.Where(c => c.Type == "permission");
            //if (permissionClaims != null && permissionClaims.Any(c => c.Value == requiredPermission))
            //{
            //    context.Succeed(requirement);
            //    return;
            //}

            if (!AuthorizationHelper.TryGetUserIdFromAuthorizationContext(context, out string userId))
                return;

            if (!AuthorizationHelper.TryGetRoleFromAuthorizationContext(context, out string roleName))
                return;

            var user = await _context.Users.FindAsync(userId);
            if (user == null || !await _userManager.IsInRoleAsync(user, roleName))
                return;

            var roleAccessModule = await _context.RoleAccessModules
                .Include(ram => ram.Role)
                .Include(ram => ram.Module)
                .FirstOrDefaultAsync(ram => ram.Role.Name == roleName && ram.Module.Name == requirement.Module);

            if (roleAccessModule == null)
                return;

            var hasPermission = requirement.Permission switch
            {
                "Read" => roleAccessModule.CanRead,
                "Write" => roleAccessModule.CanWrite,
                "Update" => roleAccessModule.CanUpdate,
                "Delete" => roleAccessModule.CanDelete,
                _ => false
            };

            if (!hasPermission)
                return;

            context.Succeed(requirement);
        }
    }
}
