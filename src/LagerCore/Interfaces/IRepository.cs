using System.Collections.Generic;

namespace LagerCore.Core.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Add an item to the repo
        /// </summary>
        /// <param name="itemToAdd">The item to be added</param>
        void Add(T itemToAdd);

        /// <summary>
        /// Remove an item from the repo
        /// </summary>
        /// <param name="itemToRemove">The item to be removed from the repo</param>
        void Remove(T itemToRemove);

        /// <summary>
        /// Get an itme from the repo
        /// </summary>
        /// <param name="key">The key of the item being returned</param>
        /// <returns>A copy of the item from the repo</returns>
        T Get(int key);

        /// <summary>
        /// Get all the items from the repository
        /// </summary>
        /// <returns>A list of items</returns>
        IList<T> GetAll();

        /// <summary>
        /// Check to see if the repo has an item already
        /// </summary>
        /// <param name="key">The key of the item to besearched for</param>
        /// <returns>True if the item is in the repo, false if not</returns>
        bool Contains(int key);

        /// <summary>
        /// Replace an item in the repo
        /// </summary>
        /// <param name="keyOfItemToReplace">The item to be replaced; will be deleted</param>
        /// <param name="newItem">The new item to be added to the repo</param>
        void Replace(int keyOfItemToReplace, T newItem);
    }
}
