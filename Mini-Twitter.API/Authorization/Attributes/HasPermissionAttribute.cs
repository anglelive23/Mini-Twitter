namespace Mini_Twitter.API.Authorization.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string module, string permission)
        {
            // under the hood, this sets the Policy property of the AuthorizeAttribute and fires up the AuthorizationPolicyProvider into Authorization Handler flow
            Policy = $"{module}.{permission}";
        }
    }
}
