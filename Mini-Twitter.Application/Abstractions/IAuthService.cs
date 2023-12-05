using Mini_Twitter.Application.Models;
using Mini_Twitter.Domain.Entities;

namespace Mini_Twitter.Application.Abstractions
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<AuthModel> RequestTokenAsync(TokenRequestModel model);
        Task<AuthModel> GetTokenAsync(ApplicationUser user);
        Task<AuthModel> RefreshTokenAsync(string refreshToken);
        Task<AuthModel> RevokeAndGenerate(string refreshToken);
        Task<bool> RevokeTokenAsync(string token);
    }
}
