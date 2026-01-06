using Mini_Twitter.Application.Extensions;

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

            if (result.IsFailure)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.Value.RefreshToken, result.Value.RefreshTokenExpiration);

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

            if (result.IsFailure)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.Value.RefreshToken, result.Value.RefreshTokenExpiration);
            return Ok(result);

            //return result.Match<AuthModel, IActionResult>(
            //    authModel =>
            //    {
            //        SetRefreshTokenInCookie(authModel.RefreshToken, authModel.RefreshTokenExpiration);
            //        return Ok(authModel);
            //    },
            //    error => BadRequest(error)
            //);
        }
        #endregion
    }
}
