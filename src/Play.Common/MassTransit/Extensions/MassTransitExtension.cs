using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Play.Common.Settings.Extensions;

using System.Reflection;

namespace Play.Common.MassTransit.Extensions
{
    /// <summary>
    /// <see cref="MassTransitExtension"/> provides support for MassTransit with RabbitMQ dependency injection.
    /// </summary>
    public static class MassTransitExtension
    {
        /// <summary>
        /// Add the RabbitMQ service broker to the services collection.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        /// <returns>The collection of service descriptors, with the MongoDB database service.</returns>
        /// /// <exception cref="InvalidOperationException">If any configuration is missing.</exception>
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                var assembly = Assembly.GetEntryAssembly();

                configure.AddConsumers(assembly);

                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetRequiredService<IConfiguration>();
                    if (configuration is null)
                    {
                        throw new InvalidOperationException($"No '{nameof(IConfiguration)}' service found in service provider.");
                    }

                    var rabbitMQSettings = configuration.GetRabbitMQSettings();

                    var serviceSettings = configuration.GetServiceSettings();

                    configurator.Host(rabbitMQSettings.Host);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                    configurator.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });

            return services;
        }
    }
}