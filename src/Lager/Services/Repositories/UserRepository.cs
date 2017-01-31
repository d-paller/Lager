using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Lager.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Lager.Services.Repositories
{

    public class UserRepository:IUserRepository 
    {
        private readonly DBContext _context = null;
        public UserRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }
        public async Task Add(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> Contains(string uname)
        {
            var filter = Builders<User>.Filter.Eq("Username", uname);
            List<User> result = await _context.Users.Find(filter).ToListAsync();
            if (result.Count == 0)
                return false;
            else
                return true;
        }

        public async Task<User> Get(string name)
        {
            var filter = Builders<User>.Filter.Eq("Username", name);
            return await _context.Users
                             .Find(filter)
                             .FirstOrDefaultAsync();
        }

        public async Task<IList<User>> GetAll()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public void Remove(IUser itemToRemove)
        {
            throw new NotImplementedException();
        }

        public void Replace(int keyOfItemToReplace, IUser newItem)
        {
            throw new NotImplementedException();
        }
    }
}
