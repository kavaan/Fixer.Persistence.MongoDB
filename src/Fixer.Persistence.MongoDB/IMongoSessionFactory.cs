using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Fixer.Persistence.MongoDB
{
    public interface IMongoSessionFactory
    {
        Task<IClientSessionHandle> CreateAsync();
    }
}
