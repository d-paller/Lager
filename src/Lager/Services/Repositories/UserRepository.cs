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
        private readonly PartContext _context = null;
        public UserRepository(IOptions<Settings> settings)
        {
            _context = new PartContext(settings);
        }
        public async Task Add(User itemToAdd)
        {
            await _context.Users.InsertOneAsync(itemToAdd);
        }

        public bool Contains(string uname)
        {
            var filter = Builders<User>.Filter.Eq("Username", uname);
            long result = _context.Users.Find(filter).CountAsync;
            if (result == 0)
                return false;
            else
                return true;
        }

        public IUser Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
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
