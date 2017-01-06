using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Lager.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
/*
namespace Lager.Services.Repositories
{
    public class PartRepository : IRepository<Part>
    {
        private readonly PartContext _context = null;
        public PartRepository(IOptions<Settings> settings)
        {
            _context = new PartContext(settings);
        }
        public async Task<IEnumerable<Part>> GetAllNotes()
        {
            return await _context.Parts.Find(_ => true).ToListAsync();
        }

        public async Task<Part> GetPart(string id)
        {
            var filter = Builders<Part>.Filter.Eq("Id", id);
            return await _context.Parts
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task AddPart(Part item)
        {
            await _context.Parts.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemovePart(string id)
        {
            return await _context.Parts.DeleteOneAsync(
                         Builders<Part>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdatePart(string id, string body)
        {
            var filter = Builders<Part>.Filter.Eq(s => s.Id, id);
            var update = Builders<Part>.Update
                                .Set(s => s.Body, body)
                                .CurrentDate(s => s.UpdatedOn);
            return await _context.Parts.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdateNote(string id, Note item)
        {
            return await _context.Notes
                                 .ReplaceOneAsync(n => n.Id.Equals(id)
                                                     , item
                                                     , new UpdateOptions { IsUpsert = true });
        }

        public async Task<DeleteResult> RemoveAllNotes()
        {
            return await _context.Notes.DeleteManyAsync(new BsonDocument());
        }
    }
}
*/