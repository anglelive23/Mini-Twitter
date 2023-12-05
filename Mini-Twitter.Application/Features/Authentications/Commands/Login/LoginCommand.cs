using MediatR;
using Mini_Twitter.Application.Models;

namespace Mini_Twitter.Application.Features.Authentications.Commands.Login
{
    public class LoginCommand : IRequest<AuthModel?>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
