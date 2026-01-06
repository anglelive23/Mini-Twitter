using Microsoft.AspNetCore.Authorization;
using Mini_Twitter.Application.Authorization.Requirements;

namespace Mini_Twitter.Infrastructure.Authorization.Policy
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
            => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
            => Task.FromResult<AuthorizationPolicy?>(null);

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (string.IsNullOrWhiteSpace(policyName) || !policyName.Contains("."))
                return Task.FromResult<AuthorizationPolicy?>(null);

            var parts = policyName.Split('.');
            var module = parts[0];
            var permission = parts[1];

            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(module, permission))
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }
    }
}
