using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lager.Models;
using MongoDB.Bson.Serialization;

/// <summary>
/// inside <> are the collections that are in the database
/// </summary>
namespace Lager.Models
{
    public class DataAccess
    {
        MongoClient client;
        IMongoDatabase _db;
        IMongoCollection<Part> collection;

        public DataAccess()
        {
            
            client = new MongoClient("mongodb://localhost:27017");
            _db = client.GetDatabase("LagerDB"); ;   //the database name
            collection = _db.GetCollection<Part>("Parts");
        }

        public List<Part> GetParts()
        {
            return collection.Find(new BsonDocument()).ToList();
        }


        public List<Part> GetPart(String name, int partID)
        {
            var builder = Builders<Part>.Filter;
            var filter = builder.Eq("Name", name)&builder.Eq("PartID", partID);
            var p = collection.Find(filter).ToList();
            return p;
        }
        
        public Part Create(Part p)
        {
            collection.InsertOne(p);
            return p;
        }

        public void Update(ObjectId id, Part p)
        {
            p.Id = id;
            var res = Query<Part>.EQ(pd => pd.Id, id);
            var operation = Update<Part>.Replace(p);
            _db.GetCollection<Part>("Parts").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Part>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Part>("Parts").Remove(res);
        }
        
    }
}

