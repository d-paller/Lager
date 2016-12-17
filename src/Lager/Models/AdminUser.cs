using Lager.CoreFiles.Interfaces;

namespace LagerCore.Core.Models
{
    public class AdminUser : IUser
    {
        /// <summary>
        /// The hashed and ecrypted password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The hashed username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }

        private bool Admin = true;

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
    }
}
