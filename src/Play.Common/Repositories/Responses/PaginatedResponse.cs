namespace Play.Common.Repositories.Responses
{
    /// <summary>
    /// Represents a paginated response for a query.
    /// </summary>
    /// <typeparam name="T">paginated entity type.</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// Gets or sets the data items for the current page.
        /// </summary>
        /// <value>The data items for the current page.</value>
        public IReadOnlyList<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the total count of items available.
        /// </summary>
        /// <value>The total count of items available</value>
        public long TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        /// <value>The number of items per page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        /// <value>The current page number.</value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets total pages returned by search.
        /// </summary>
        /// <value>The total pages returned by search.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the index of the first item on the current page.
        /// </summary>
        /// <value>The index of the first item on the current page.</value>
        public int ItemsFrom { get; set; }

        /// <summary>
        /// Gets or sets the index of the last item on the current page.
        /// </summary>
        /// <value>The index of the last item on the current page.</value>
        public int ItemsTo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedResponse{T}"/> class with specified parameters.
        /// </summary>
        /// <param name="pageNumber">The current page number returned</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="totalCount">The total count of items available.</param>
        /// <param name="items">The data items for the current page.</param>
        public PaginatedResponse(int pageNumber, int pageSize, long totalCount, IReadOnlyList<T> items)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }
    }
}