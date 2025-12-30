using Microsoft.Extensions.Configuration;

namespace Play.Common.Settings.Extensions
{
    /// <summary>
    /// <see cref="ServiceSettingsExtension"/> provides extension methods for <see cref="ServiceSettings"/>.
    /// </summary>
    public static class ServiceSettingsExtension
    {
        /// <summary>
        /// Gets the <see cref="ServiceSettings"/> from the configuration.
        /// </summary>
        /// <param name="configuration">An instance of application configuration properties.</param>
        /// <returns>The <see cref="ServiceSettings"/> from the configuration.</returns>
        /// <exception cref="InvalidOperationException">If any configuration is missing.</exception>
        public static ServiceSettings GetServiceSettings(this IConfiguration configuration)
        {
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings))
                                               .Get<ServiceSettings>();

            if (serviceSettings is null)
            {
                throw new InvalidOperationException($"No '{nameof(ServiceSettings)}' section found in configuration.");
            }

            if (string.IsNullOrEmpty(serviceSettings.ServiceName) || string.IsNullOrWhiteSpace(serviceSettings.ServiceName))
            {
                throw new InvalidOperationException($"No service name defined in '{nameof(ServiceSettings)}' section found in configuration.");
            }

            if (string.IsNullOrEmpty(serviceSettings.Authority) || string.IsNullOrWhiteSpace(serviceSettings.Authority))
            {
                throw new InvalidOperationException($"No authority defined in '{nameof(ServiceSettings)}' section found in configuration.");
            }

            return serviceSettings;
        }
    }
}