namespace Lager.Interfaces
{
    /// <summary>
    /// The interface for all users.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Checks to see if the user is an admin or not
        /// </summary>
        /// <returns>True if user is admin, false if not</returns>
        bool IsAdmin();

        /// <summary>
        /// Checks to make sure the form data entered is Valid
        /// </summary>
        /// <param name="userName">The hashed username of the user</param>
        /// <param name="password">The hashed password of the user</param>
        /// <returns></returns>
        bool IsValid(string userName, string password);

    }
}
