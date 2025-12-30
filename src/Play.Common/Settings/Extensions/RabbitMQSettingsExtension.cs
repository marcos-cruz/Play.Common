using Microsoft.Extensions.Configuration;

namespace Play.Common.Settings.Extensions
{
    /// <summary>
    /// <see cref="RabbitMQSettingsExtension"/> provides extension methods for <see cref="RabbitMQSettings"/>.
    /// </summary>
    public static class RabbitMQSettingsExtension
    {
        /// <summary>
        /// Gets the <see cref="RabbitMQSettings"/> from the configuration.
        /// </summary>
        /// <param name="configuration">An instance of application configuration properties.</param>
        /// <returns>The <see cref="RabbitMQSettings"/> from the configuration.</returns>
        /// <exception cref="InvalidOperationException">If any configuration is missing.</exception>
        public static RabbitMQSettings GetRabbitMQSettings(this IConfiguration configuration)
        {
            var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings))
                                                .Get<RabbitMQSettings>();
            if (rabbitMQSettings is null)
            {
                throw new InvalidOperationException($"No '{nameof(RabbitMQSettings)}' section found in configuration.");
            }

            if (string.IsNullOrEmpty(rabbitMQSettings.Host) || string.IsNullOrWhiteSpace(rabbitMQSettings.Host))
            {
                throw new InvalidOperationException($"No host address defined in '{nameof(RabbitMQSettings)}' section found in configuration.");
            }


            return rabbitMQSettings;
        }
    }
}