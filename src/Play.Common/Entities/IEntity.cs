namespace Play.Common.Entities
{
    /// <summary>
    /// Defines a contract for the base class for creating a domain entity, which contains an ID.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        /// <value>The unique identifier for the entity.</value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        /// <value>The date and time when the entity was created.</value>
        DateTimeOffset CreatedDate { get; }

        /// <summary>
        /// Gets a value indicating whether the entity is marked as deleted.
        /// </summary>
        /// <value><c>true</c> if the entity is deleted; otherwise, <c>false</c>.</value>
        bool Deleted { get; }
    }
}