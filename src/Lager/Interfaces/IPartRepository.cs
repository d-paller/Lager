using System.Collections.Generic;
using Lager.Models;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Lager.Interfaces
{
    public interface IPartRepository
    {
        Task<IEnumerable<Part>> GetAllPart();
        Task<Part> GetPart(string id);
        Task AddPart(Part item);
        Task<DeleteResult> RemovePart(string id);
        Task<ReplaceOneResult> UpdatePart(string id, Part item);

        // demo interface - full document update
        //Task<ReplaceOneResult> UpdatePartDocument(string id, Part item);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllParts();
    }
}
