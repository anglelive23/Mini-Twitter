namespace Mini_Twitter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AuthBaseModel
    {
        #region Fields and Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
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
