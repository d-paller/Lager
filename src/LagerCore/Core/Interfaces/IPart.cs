namespace LagerCore.Core.Interfaces
{
    interface IPart
    {
        /// <summary>
        /// Checks to see if the part is active or not
        /// </summary>
        /// <returns>True if the item is active, false if not</returns>
        bool IsActive();

        /// <summary>
        /// Navigate to the webpage where the part was purchased.  If not URL is given, this will not be implimented.
        /// </summary>
        void RepurchasePart();
    }
}
