using System;
using System.Collections.Generic;
using System.Linq;
using Lager.Models;
using System.Threading.Tasks;

namespace Lager.Interfaces
{
    interface IUserRepository
    {
        //Task<IEnumerable<User>> GetAllUser();
        Task Add(User user);
        Task<bool> Contains(string uname);
        Task<User> Get(string name);
    }
}
