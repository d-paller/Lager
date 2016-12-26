using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        private bool Admin;

        /// <summary>
        /// If the user is active or not.  The default value is true.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Checks to see if the user is an admin or not.
        /// </summary>
        /// <returns>True if the user is an admin, false if not.</returns>
        public bool IsAdmin()
        {
            return Admin;
        }

        public bool IsValid(string username, string password)
        {
            return true;
        }
    }
}
