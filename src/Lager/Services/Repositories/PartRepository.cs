using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Services.Repositories
{
    public class PartRepository : IRepository<IPart>
    {
        public void Add(IPart itemToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Contains(int key)
        {
            throw new NotImplementedException();
        }

        public IPart Get(int key)
        {
            throw new NotImplementedException();
        }

        public IList<IPart> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(IPart itemToRemove)
        {
            throw new NotImplementedException();
        }

        public void Replace(int keyOfItemToReplace, IPart newItem)
        {
            throw new NotImplementedException();
        }
    }
}
