using Lager.Interfaces;
using Lager.Models;
using Microsoft.AspNetCore.Identity;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Lager.Services
{
    public class SCryptPasswordHasher : IPasswordHasher<User>
    {

        private readonly ScryptEncoder _encoder;

        public SCryptPasswordHasher()
        {
            _encoder = new ScryptEncoder(3 ^ 16, 10, 1);
        }

        /// <summary>
        /// Hashes the password
        /// </summary>
        /// <param name="user">The user that the password belongs to</param>
        /// <param name="password">The raw string</param>
        /// <returns>A hashed and salted string</returns>
        public string HashPassword(User user, string password)
        {
            return _encoder.Encode(SaltPassword(user, password));
        }

        /// <summary>
        /// Verifies the hashed password
        /// </summary>
        /// <param name="user">The user that the password belongs to</param>
        /// <param name="hashedPassword">The password from the DB</param>
        /// <param name="providedPassword">The password given by the user</param>
        /// <returns></returns>
        public bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if(_encoder.Compare(SaltPassword(user, providedPassword), hashedPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // ----- Private Methods ----- //
        private static string SaltPassword(User user, string password)
        {
            return user.Salt + password;   
        }
    }
}
