using Mini_Twitter.Application.Common;

namespace Mini_Twitter.Application.Features.Authentications.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthModel>>
    {
        #region Fields and Properties
        private readonly IAuthService _authService;
        #endregion

        #region Constructors
        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        #endregion

        #region Interface Implementation
        public async Task<Result<AuthModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            var authModel = await _authService.RegisterAsync(request.Adapt<RegisterModel>());
            return authModel;
        }
        #endregion
    }
}
