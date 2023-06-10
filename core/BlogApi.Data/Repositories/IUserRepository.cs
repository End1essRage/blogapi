using BlogApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByUserNameAsync(string userName);
        Task CreateUserAsync(User user);
    }
}
