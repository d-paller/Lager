using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lager.Interfaces
{
    public interface IRepository<T>
    {
    Task<IEnumerable<T>> GetAllNotes();
        Task<T> GetT(string id);
        Task AddNote(T item);
        Task<DeleteResult> RemoveT(string id);
        Task<UpdateResult> UpdateT(string id, string body);

        // demo interface - full document update
        Task<ReplaceOneResult> UpdateTDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllT();
    }
}
}
