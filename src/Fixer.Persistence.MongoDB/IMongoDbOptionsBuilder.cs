using System;
using System.Collections.Generic;
using System.Text;

namespace Fixer.Persistence.MongoDB
{
    public interface IMongoDbOptionsBuilder
    {
        IMongoDbOptionsBuilder WithConnectionString(string connectionString);
        IMongoDbOptionsBuilder WithDatabase(string database);
        IMongoDbOptionsBuilder WithSeed(bool seed);
        MongoDbOptions Build();
    }
}
