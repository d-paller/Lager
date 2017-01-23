using Lager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Interfaces
{
    public interface IUserRepository
    {
        Task<IUser> GetUserByUsername(string userName);

        Task<IUser> GetUserById(int Id);

        Task AddUser(IUser user);

        Task<bool> UserExists(string userName);

        Task<bool> UserExists(int userId);
    }
}
