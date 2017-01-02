using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lager.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Lager.Models
{
    public class PartContext
    {
        private readonly IMongoDatabase _database = null;

        public PartContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Part> Parts
        {
            get
            {
                return _database.GetCollection<Part>("Parts");
            }
        }
    }
}
