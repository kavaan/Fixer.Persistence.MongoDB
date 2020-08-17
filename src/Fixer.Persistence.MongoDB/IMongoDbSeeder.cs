using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Fixer.Persistence.MongoDB
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}
