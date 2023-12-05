namespace Mini_Twitter.Bases
{
    public class AuthBaseModel : ControllerBase
    {
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
    }
}
