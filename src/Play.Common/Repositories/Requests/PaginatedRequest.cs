using System.ComponentModel;

using Play.Common.Repositories.Enums;

namespace Play.Common.Repositories.Requests
{
    /// <summary>
    /// Represents a request for a paginated query.
    /// </summary>
    public class PaginatedRequest
    {
        private const int MaximumPageSize = 50;
        private const int MinimumPageSize = 5;
        private const int DefaultPageSize = 10;

        private int _pageSize = DefaultPageSize;

        /// <summary>
        /// Gets or sets the page number you want to search for.
        /// </summary>
        /// <value>The page number requested. Default is 1.</value>
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        /// <value>The number of items per page. Minimum is 5, Maximum is 50. Default is 10.</value>
        [DefaultValue(DefaultPageSize)]
        public int PageSize
        {
            get { return _pageSize; }

            set
            {
                if (value < MinimumPageSize)
                {
                    value = MinimumPageSize;
                }
                else if (value > MaximumPageSize)
                {
                    value = MaximumPageSize;
                }

                _pageSize = value;
            }
        }

        /// <summary>
        /// The order in which to sort the results, Ascending or Descending.
        /// </summary>
        /// [DefaultValue(SortOrder.Descending)]
        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
    }
}