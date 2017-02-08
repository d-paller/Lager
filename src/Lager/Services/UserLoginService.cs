using Lager.Models;
using Lager.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;
using Lager.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lager.Services
{
    public class UserLoginService
    {
        private IUserRepository _userRepo;
        private SCryptPasswordHasher _hasher = new SCryptPasswordHasher();

        public UserLoginService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        
        public async Task<bool> UserIsValid(User givenUser)
        {
            User DbUser = await _userRepo.GetUserByUsername(givenUser.Username);
            return _hasher
                .VerifyHashedPassword(givenUser, DbUser.Password, givenUser.Password) == PasswordVerificationResult.Success 
                ? true : false;
        }

    }
}
