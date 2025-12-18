namespace Play.Common.Settings
{
    /// <summary>
    /// Settings for RabbitMQ connection.
    /// </summary>
    public class RabbitMQSettings
    {
        /// <summary>
        /// The host where RabbitMQ is running.
        /// </summary>
        /// <value>The host address where RabbitMQ is running.</value>
        public string Host { get; init; } = default!;
    }
}