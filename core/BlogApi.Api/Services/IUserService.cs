using BlogApi.Data.Models;

namespace BlogApi.Api.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> FindByUserNameAsync(string userName);
    }
}
