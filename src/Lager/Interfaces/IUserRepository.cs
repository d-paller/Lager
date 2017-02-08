using Lager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string userName);

        Task<User> GetUserById(int Id);

        Task AddUser(User user);

        Task<bool> UserExists(string userName);

        Task<bool> UserExists(int userId);
    }
}
