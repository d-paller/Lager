using System.Collections.Generic;
using Lager.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;

namespace Lager.Interfaces
{
    public interface IPartRepository
    {
        IQueryable<Part> GetAllPart();
        Task<Part> GetPart(string name, int id);
        Task AddPart(Part item);
        Task<DeleteResult> RemovePart(string name, int id);
        Task<ReplaceOneResult> UpdatePart(string id, Part item);
        Task<List<Part>> GetAllParts(string n);

        // demo interface - full document update
        //Task<ReplaceOneResult> UpdatePartDocument(string id, Part item);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllParts();
    }
}
