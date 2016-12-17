using LagerCore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagerCore.Core.Services.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        public void Add(IUser itemToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Contains(int key)
        {
            throw new NotImplementedException();
        }

        public IUser Get(int key)
        {
            throw new NotImplementedException();
        }

        public IList<IUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(IUser itemToRemove)
        {
            throw new NotImplementedException();
        }

        public void Replace(int keyOfItemToReplace, IUser newItem)
        {
            throw new NotImplementedException();
        }
    }
}
