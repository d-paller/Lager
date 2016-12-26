using Lager.Interfaces;
using Microsoft.AspNetCore.Identity;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Services
{
    public class SCryptPasswordHasher : IPasswordHasher<IUser>
    {

        private readonly ScryptEncoder _encoder;

        public string HashPassword(IUser user, string password)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(IUser user, string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
