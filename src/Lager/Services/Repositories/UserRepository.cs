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

    public class UserRepository :IUserRepository
    {
        private readonly DBContext _context = null;
        public UserRepository()
        {
            _context = new DBContext();
        }
        public UserRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<User> GetUserById(int Id)
        {
            var filter = Builders<User>.Filter.Eq("Id", Id);
            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            var filter = Builders<User>.Filter.Eq("UserName", userName);
            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(int userId)
        {
            var filter = Builders<User>.Filter.Eq("Id", userId);
            return await _context.Users.Find(filter).FirstOrDefaultAsync() == null ? false : true;
        }

        public async Task<bool> UserExists(string userName)
        {
            var filter = Builders<User>.Filter.Eq("UserName", userName);
            return await _context.Users.Find(filter).FirstOrDefaultAsync() == null ? false : true;
        }

    }
}
