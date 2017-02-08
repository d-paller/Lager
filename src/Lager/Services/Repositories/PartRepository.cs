﻿using Lager.Interfaces;
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
        //return everything in the database
        public async Task<List<Part>> GetAllPart()
        {
            return await _context.Parts.Find(_ => true).ToListAsync();
        }
        //Get all the same kind of parts
        public async Task<IEnumerable<IPart>> GetAllParts(string n)
        {
            var filter = Builders<IPart>.Filter.Eq("Name",n);
            return await _context.Parts.Find(filter).ToListAsync();
        }

        public async Task<IPart> GetPart(string name, int id)
        {
            var filter = Builders<IPart>.Filter.Eq("Id", id);
            return await _context.Parts
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task AddPart(IPart item)
        {
            await _context.Parts.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemovePart(string name, int id)
        {
            var filter = Builders<Part>.Filter.Eq("PartId", id) & Builders<Part>.Filter.Eq("Name", name);
            return await _context.Parts.DeleteOneAsync(filter);
        }

        public async Task<ReplaceOneResult> UpdatePart(string id, IPart item)
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
    }
}

