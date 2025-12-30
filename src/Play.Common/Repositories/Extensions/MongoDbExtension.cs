using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

using Play.Common.Entities;
using Play.Common.Settings.Extensions;

namespace Play.Common.Repositories.Extensions
{
    /// <summary>
    /// <see cref="MongoDbExtension"/> provides support for MongoDB dependency injection.
    /// </summary>
    public static class MongoDbExtension
    {
        /// <summary>
        /// Add the MongoDB repository to the services collection.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        /// <returns>The collection of service descriptors, with the MongoDB database service.</returns>
        /// <exception cref="InvalidOperationException">If any configuration is missing.</exception>
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            AddMongoDbSerializers();

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                if (configuration is null)
                {
                    throw new InvalidOperationException($"No '{nameof(IConfiguration)}' service found in service provider.");
                }

                var serviceSettings = configuration.GetServiceSettings();

                var mongoDbSettings = configuration.GetMongoDbSettings();

                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        /// <summary>
        /// Adds a MongoDB repository to the collection of services.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        /// <param name="collectionName">Name of the collection that identifies the entity in the repository.</param>
        /// <typeparam name="T">Type of entity to which the repository should be added.</typeparam>
        /// <returns>The collection of service descriptors, with the <see cref="MongoRepository{T}"/> service.</returns>
        /// <exception cref="InvalidOperationException">If any configuration is missing.</exception>
        /// <example>
        /// <code>
        /// services.AddMongoRepository&lt;Product&gt;("products");
        /// </code>
        /// </example> 
        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
            if (string.IsNullOrEmpty(collectionName) || string.IsNullOrWhiteSpace(collectionName))
            {
                throw new InvalidOperationException($"No '{nameof(collectionName)}' was provided.");
            }

            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {
                var database = serviceProvider.GetRequiredService<IMongoDatabase>();
                if (database is null)
                {
                    throw new InvalidOperationException($"No '{nameof(IMongoDatabase)}' service found in service provider.");
                }

                return new MongoRepository<T>(database, collectionName);
            });

            return services;
        }

        /// <summary>
        /// Add more friendly serializers for MongoDb.
        /// </summary>
        private static void AddMongoDbSerializers()
        {
            BsonSerializer.TryRegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.TryRegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.TryRegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.TryRegisterSerializer(new DecimalSerializer(MongoDB.Bson.BsonType.String));
        }
    }
}