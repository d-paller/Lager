using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Lager.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace Lager.Services.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly DBContext _context = null;
        public PartRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }
        public Task<List<Part>> GetAllPartIn()
        {
            return _context.Parts.Find(_ => true).ToListAsync();
        }
        //return everything in the database
        public async Task<IQueryable<Part>> GetAllPart()
        {
            var all = await _context.Parts.FindAsync(_ => true);
            return all.ToEnumerable().AsQueryable();
        }
        //Get all the same kind of parts
        public IQueryable<Part> GetAllPartsByName(string n)
        {
            var filter = Builders<Part>.Filter.Eq("Name",n);
            return _context.Parts.Find(filter).ToEnumerable().AsQueryable();
        }
        public  IQueryable<Part> GetAllPartsByCategory(string n)
        {
            var filter = Builders<Part>.Filter.Eq("Category", n);
            return _context.Parts.Find(filter).ToEnumerable().AsQueryable();
        }
        public  IQueryable<Part> GetAllPartsByVendor(string n)
        {
            var filter = Builders<Part>.Filter.Eq("Vendor", n);
            return _context.Parts.Find(filter).ToEnumerable().AsQueryable();
        }
        public IQueryable<Part> GetAllPartsByHolder(string n)
        {
            var filter = Builders<Part>.Filter.Eq("Holder", n);
            return _context.Parts.Find(filter).ToEnumerable().AsQueryable();
        }
        public async Task<List<Part>> GetAllParts(string n)
        {
            var filter = Builders<Part>.Filter.Eq("Name", n);
            return await _context.Parts.Find(filter).ToListAsync();
        }

        public async Task<Part> GetPart(string name, int id)
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

        public async Task<DeleteResult> RemovePart(string name, int id)
        {
            var filter = Builders<Part>.Filter.Eq("PartId", id) & Builders<Part>.Filter.Eq("Name", name);
            return await _context.Parts.DeleteOneAsync(filter);
        }

        public async Task<ReplaceOneResult> UpdatePart(string id, Part item)
        {
            return await _context.Parts
                                 .ReplaceOneAsync(n => n.Id.Equals(id)
                                                     , item
                                                     , new UpdateOptions { IsUpsert = true });
        }

        public async Task<DeleteResult> RemoveAllParts()
        {
            return await _context.Parts.DeleteManyAsync(new BsonDocument());
        }

        //For sidebar navigation
        //public async Task<IEnumerable<IEnumerable<Part>>> GetAllByCategoryAsync()
        //{
            //List<Part> categories = await _context.Parts.Find(_ => true).ToListAsync();
            //List<List<Part>> list = new List<List<Part>>();

            //for (int i = 0; i < categories.Count; i++)
            //{
                //var filter = Builders<Part>.Filter.Eq("Section", categories[i].Category);
                //var newList = await _context.Parts.Find(filter).ToListAsync();
              //  list.Add(newList);
            //}

          //  return list;
        //}
    }
}

