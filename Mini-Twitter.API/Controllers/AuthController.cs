namespace Mini_Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseControllerModel
    {
        #region Constructors
        public AuthController(IMediator mediator) : base(mediator) { }
        #endregion

        #region Authentication Endpoints
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _mediator.Send(new RegisterCommand
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            });

            if (!result.IsAuthenticated)
                return StatusCode(StatusCodes.Status400BadRequest, $"{result.Message}");

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _mediator
                .Send(new LoginCommand
                {
                    Email = model.Email,
                    Password = model.Password
                });

            if (!result.IsAuthenticated)
                return StatusCode(StatusCodes.Status400BadRequest, $"{result.Message}");

            if (!string.IsNullOrEmpty(result.Token))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        #endregion
    }
}
