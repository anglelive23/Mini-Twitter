using Microsoft.AspNetCore.Authorization;

namespace Mini_Twitter.Application.Authorization.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Module { get; set; }
        public string Permission { get; }

        public PermissionRequirement(string module, string permission)
        {
            Module = module;
            Permission = permission;
        }
    }
}
