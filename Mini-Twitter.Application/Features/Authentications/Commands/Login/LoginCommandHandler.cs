using Mini_Twitter.Application.Common;

namespace Mini_Twitter.Application.Features.Authentications.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthModel>>
    {
        #region Fields and Properties
        private readonly IAuthService _authService;
        #endregion

        #region Constructors
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        #endregion

        #region Interface Implementation
        public async Task<Result<AuthModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            var authModel = await _authService.LoginAsync(request.Adapt<LoginModel>());
            return authModel;
        }
        #endregion
    }
}
