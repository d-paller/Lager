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

    }
}
