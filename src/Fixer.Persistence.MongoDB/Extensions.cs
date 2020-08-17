using System;
using Fixer.Persistence.MongoDB.Builders;
using Fixer.Persistence.MongoDB.Factories;
using Fixer.Persistence.MongoDB.Initializers;
using Fixer.Persistence.MongoDB.Repositories;
using Fixer.Persistence.MongoDB.Seeders;
using Fixer.Types;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Fixer.Persistence.MongoDB
{
    public static class Extensions
    {
        private const string SectionName = "mongo";
        private const string RegistryName = "persistence.mongoDb";

        public static IFixerBuilder AddMongo(this IFixerBuilder builder, string sectionName = SectionName,
            IMongoDbSeeder seeder = null)
        {
            var mongoOptions = builder.GetOptions<MongoDbOptions>(sectionName);
            return builder.AddMongo(mongoOptions, seeder);
        }

        public static IFixerBuilder AddMongo(this IFixerBuilder builder, Func<IMongoDbOptionsBuilder,
            IMongoDbOptionsBuilder> buildOptions, IMongoDbSeeder seeder = null)
        {
            var mongoOptions = buildOptions(new MongoDbOptionsBuilder()).Build();
            return builder.AddMongo(mongoOptions, seeder);
        }

        public static IFixerBuilder AddMongo(this IFixerBuilder builder, MongoDbOptions mongoOptions,
            IMongoDbSeeder seeder = null)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddSingleton(mongoOptions);

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            builder.Services.AddTransient(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Database);
            });

            builder.Services.AddTransient<IMongoDbInitializer, MongoDbInitializer>();
            builder.Services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

            if (seeder is null)
            {
                builder.Services.AddSingleton<IMongoDbSeeder, MongoDbSeeder>();
            }
            else
            {
                builder.Services.AddSingleton(seeder);
            }

            builder.AddInitializer<IMongoDbInitializer>();

            return builder;
        }

        public static IFixerBuilder AddMongoRepository<TEntity, TIdentifiable>(this IFixerBuilder builder,
            string collectionName)
            where TEntity : IIdentifiable<TIdentifiable>
        {
            builder.Services.AddTransient<IMongoRepository<TEntity, TIdentifiable>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoRepository<TEntity, TIdentifiable>(database, collectionName);
            });

            return builder;
        }
    }
}
