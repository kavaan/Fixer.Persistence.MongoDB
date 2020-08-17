using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fixer.Persistence.MongoDB.Factories
{
    internal sealed class MongoSessionFactory : IMongoSessionFactory
    {
        private readonly IMongoClient _client;

        public MongoSessionFactory(IMongoClient client)
            => _client = client;

        public Task<IClientSessionHandle> CreateAsync()
            => _client.StartSessionAsync();
    }
}
