namespace Mini_Twitter.Application.Features.Authentications.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthModel?>
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
        public async Task<AuthModel?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var authModel = await _authService.LoginAsync(request.Adapt<LoginModel>());

            return authModel;
        }
        #endregion
    }
}
