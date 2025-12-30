using Microsoft.Extensions.Configuration;

namespace Play.Common.Settings.Extensions
{
    /// <summary>
    /// <see cref="MongoDbSettingsExtension"/> provides extension methods for <see cref="MongoDbSettings"/>.
    /// </summary>
    public static class MongoDbSettingsExtension
    {
        /// <summary>
        /// Gets the <see cref="MongoDbSettings"/> from the configuration.
        /// </summary>
        /// <param name="configuration">An instance of application configuration properties.</param>
        /// <returns>The <see cref="MongoDbSettings"/> from the configuration.</returns>
        /// <exception cref="InvalidOperationException">If any configuration is missing.</exception>
        public static MongoDbSettings GetMongoDbSettings(this IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings))
                                               .Get<MongoDbSettings>();

            if (mongoDbSettings is null)
            {
                throw new InvalidOperationException($"No '{nameof(MongoDbSettings)}' section found in configuration.");
            }

            if (string.IsNullOrEmpty(mongoDbSettings.Host) || string.IsNullOrWhiteSpace(mongoDbSettings.Host))
            {
                throw new InvalidOperationException($"No host address defined in '{nameof(MongoDbSettings)}' section found in configuration.");
            }

            if (mongoDbSettings.Port == 0)
            {
                throw new InvalidOperationException($"No host port defined in '{nameof(MongoDbSettings)}' section found in configuration.");
            }

            return mongoDbSettings;
        }
    }
}