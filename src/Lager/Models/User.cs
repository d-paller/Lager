using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Lager.Models
{
    public class User : IUser
    {
        /// <summary>
        /// The hashed and ecrypted password of the user
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The hashed username of the user
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }


        // TODO:  Change back to private
        public bool Admin;

        /// <summary>
        /// If the user is active or not.  The default value is true.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The Salt for the users password
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Checks to see if the user is an admin or not.
        /// </summary>
        /// <returns>True if the user is an admin, false if not.</returns>
        public bool IsAdmin()
        {
            return Admin;
        }

        /// <summary>
        /// Checks to see if the users data entered is valid or not
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The hashed password of the user</param>
        /// <returns>True if the data is valid, false if not</returns>
        public bool IsValid(string username, string hashedPassword)
        {
            // TODO: Add logic once database is up and add unit tests
            return true;
        }

        /// <summary>
        /// Set the salt of the user.
        /// </summary>
        private void SetSalt()
        {
            if (string.IsNullOrEmpty(Salt))
            {
                var bytes = new byte[128];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                    Salt = BitConverter.ToString(bytes);
                }
            }
        }


        /// <summary>
        /// Default Constructor will run set salt.  If no salt exists, it will create a new one.
        /// </summary>
        public User()
        {
            SetSalt();
        }


    }
}
