using Mini_Twitter.Application.Common;

namespace Mini_Twitter.Application.Features.Authentications.Commands.Login
{
    public class LoginCommand : IRequest<Result<AuthModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
