using System.Security.Claims;

namespace Mini_Twitter.API.Bases
{
    public class BaseControllerModel : ODataController
    {
        public readonly IMediator _mediator;

        public BaseControllerModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected string GetUserId()
        {
            return User.FindFirstValue("uid") ?? string.Empty;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected async Task<ApplicationUserDto?> GetUser()
        {
            // todo: implement a result object that auto return a result if a user is not found
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return new ApplicationUserDto();

            var user = await _mediator.Send(new GetUserQuery
            {
                UserId = userId,
            });

            return user;
        }
    }
}
