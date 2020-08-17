using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Fixer.Persistence.MongoDB.Seeders
{
    internal class MongoDbSeeder : IMongoDbSeeder
    {
        public async Task SeedAsync(IMongoDatabase database)
        {
            await CustomSeedAsync(database);
        }

        protected virtual async Task CustomSeedAsync(IMongoDatabase database)
        {
            var cursor = await database.ListCollectionsAsync();
            var collections = await cursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await Task.CompletedTask;
        }
    }
}
