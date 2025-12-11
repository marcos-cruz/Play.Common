namespace Play.Common.Settings
{
    /// <summary>
    /// Settings for MongoDb connection.
    /// </summary>
    public class MongoDbSettings
    {
        /// <summary>
        /// The host where MongoDb is running.
        /// </summary>
        /// <value>The host address where MongoDb is running.</value>
        public string Host { get; init; } = default!;

        /// <summary>
        /// The port where MongoDb is listening.
        /// </summary>
        /// <value>The port number where MongoDb is listening.</value>
        public int Port { get; init; }

        /// <summary>
        /// The connection string for MongoDb.
        /// </summary>
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}