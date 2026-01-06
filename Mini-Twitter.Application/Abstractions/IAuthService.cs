using Mini_Twitter.Application.Common;

namespace Mini_Twitter.Application.Abstractions
{
    public interface IAuthService
    {
        Task<Result<AuthModel>> RegisterAsync(RegisterModel model);
        Task<Result<AuthModel>> LoginAsync(LoginModel model);
        Task<Result<AuthModel>> RequestTokenAsync(TokenRequestModel model);
        Task<AuthModel> GetTokenAsync(ApplicationUser user);
        Task<AuthModel> RefreshTokenAsync(string refreshToken);
        Task<AuthModel> RevokeAndGenerate(string refreshToken);
        Task<bool> RevokeTokenAsync(string token);
    }
}
