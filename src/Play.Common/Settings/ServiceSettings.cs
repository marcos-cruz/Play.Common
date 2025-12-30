namespace Play.Common.Settings
{
    /// <summary>
    /// Settings for the service.
    /// </summary>
    public class ServiceSettings
    {
        /// <summary>
        /// The name of the service that is running.
        /// </summary>
        /// <value>Name of the service that is running</value>
        public string ServiceName { get; init; } = default!;

        /// <summary>
        /// The authority to generate access token for the service.
        /// </summary>
        /// <value></value>
        public string Authority { get; init; } = default!;
    }
}