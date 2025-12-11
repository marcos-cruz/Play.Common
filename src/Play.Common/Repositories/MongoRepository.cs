using MongoDB.Driver;

using Play.Common.Entities;
using Play.Common.Repositories.Enums;
using Play.Common.Repositories.Requests;
using Play.Common.Repositories.Responses;

using System.Linq.Expressions;

namespace Play.Common.Repositories
{
    /// <summary>
    /// <see cref="MongoRepository{T}"/> implements a generic repository that provides support for reading and
    /// writing entities using MongoDb database.
    /// </summary>
    /// <typeparam name="T">The concrete entity type deriving from this base class.</typeparam>
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> dbCollection;

        protected readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{T}"/> class.
        /// </summary>
        /// <param name="database">Represents a MongoDB database.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public virtual async Task<bool> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await dbCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> updateFilter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);

            var result = await dbCollection.ReplaceOneAsync(updateFilter, entity, cancellationToken: cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> filterDelete = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);

            var result = await dbCollection.DeleteOneAsync(filterDelete, cancellationToken: cancellationToken);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual async Task<PaginatedResponse<T>> GetAllAsync(PaginatedRequest paginatedRequest, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> getAllFilter = filterBuilder.Empty;

            getAllFilter = getAllFilter & filterBuilder.Eq(existingEntity => existingEntity.Deleted, false);

            var totalItems = await dbCollection.CountDocumentsAsync(getAllFilter, cancellationToken: cancellationToken);

            var sortDefinition = Builders<T>.Sort.Ascending(existingEntity => existingEntity.CreatedDate);
            if (paginatedRequest.SortOrder == SortOrder.Descending)
            {
                sortDefinition = Builders<T>.Sort.Descending(existingEntity => existingEntity.CreatedDate);
            }

            var items = await dbCollection.Find(getAllFilter)
                                          .Skip((paginatedRequest.PageNumber - 1) * paginatedRequest.PageSize)
                                          .Sort(sortDefinition)
                                          .Limit(paginatedRequest.PageSize)
                                          .ToListAsync(cancellationToken: cancellationToken);

            return new PaginatedResponse<T>(paginatedRequest.PageNumber, paginatedRequest.PageSize, totalItems, items);
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> getAllFilter = filter;

            getAllFilter = getAllFilter & filterBuilder.Eq(existingEntity => existingEntity.Deleted, false);

            var items = await dbCollection.Find(getAllFilter)
                                          .ToListAsync(cancellationToken: cancellationToken);

            return items;
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> getFilter = filter;

            getFilter = getFilter & filterBuilder.Eq(entity => entity.Deleted, false);

            return await dbCollection.Find(getFilter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> getByIdFilter = filterBuilder.Eq(entity => entity.Id, id);

            getByIdFilter = getByIdFilter & filterBuilder.Eq(entity => entity.Deleted, false);

            return await dbCollection.Find(getByIdFilter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}