using System.Linq.Expressions;

using Play.Common.Entities;
using Play.Common.Repositories.Requests;
using Play.Common.Repositories.Responses;

namespace Play.Common.Repositories
{
    /// <summary>
    /// <see cref="IRepository{T}"/> defines a contract for a generic repository that provides support for reading and
    /// writing entities.
    /// </summary>
    /// <typeparam name="T">Entity type being implemented the repository.</typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added to the repository.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns><c>true</c> if the entity was successfully created, otherwise <c>false</c>.</returns>
        Task<bool> CreateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated in the repository.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns><c>true</c> if the entity was successfully updated, otherwise <c>false</c>.</returns>
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted from the repository.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns><c>true</c> if the entity was successfully deleted, otherwise <c>false</c>.</returns>
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of entities, excluding those marked as deleted, and sorted by creation date.
        /// </summary>
        /// <param name="paginatedRequest">
        /// The pagination and sorting parameters, including page number, page size, and sort order.
        /// </param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A <see cref="PaginatedResponse{T}"/> object containing the paginated list of entities,
        /// along with pagination metadata.
        /// </returns>
        Task<PaginatedResponse<T>> GetAllAsync(PaginatedRequest paginatedRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> from the database that match the specified filter
        /// and are not marked as deleted.
        /// </summary>
        /// <param name="filter">
        /// An expression used to filter the entities to be retrieved.
        /// </param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of entities
        /// that match the filter and are not marked as deleted.
        /// </returns>
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the first entity of type <typeparamref name="T"/> from the collection that matches the specified
        /// filter expression and has not been marked as deleted.
        /// </summary>
        /// <param name="filter">An expression to filter the entities.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the first matching entity of type <typeparamref name="T"/>,
        /// or <c>null</c> if no entity is found.
        /// </returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by its unique identifier, ensuring that the entity
        /// has not been marked as deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the entity if found and not deleted; otherwise, <c>null</c>.
        /// </returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}