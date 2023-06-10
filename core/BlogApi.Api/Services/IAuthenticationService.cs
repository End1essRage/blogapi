using BlogApi.Api.Services.Communication;

namespace BlogApi.Api.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string userName, string password);
    }
}
