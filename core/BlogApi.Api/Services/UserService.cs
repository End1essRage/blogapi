using BlogApi.Data.Models;
using BlogApi.Data.Repositories;

namespace BlogApi.Api.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository) 
        {
            _repository = repository;
        }

        public async Task CreateUserAsync(User user)
        {
            if (await _repository.FindByUserNameAsync(user.UserName) != null)
                return;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _repository.CreateUserAsync(user);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _repository.FindByUserNameAsync(userName);
        }
    }
}
