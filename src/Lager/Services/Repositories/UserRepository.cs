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

    public class UserRepository 
    {
        private readonly DBContext _context = null;
        public UserRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }
        public async Task Add(User itemToAdd)
        {
            await _context.Users.InsertOneAsync(itemToAdd);
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

        public IUser Get(int key)
        {
            throw new NotImplementedException();
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
