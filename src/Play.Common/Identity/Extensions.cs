using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Play.Common.Identity
{
    /// <summary>
    /// Provides extension methods for identity services.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds JWT Bearer authentication to the service collection.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        /// <returns>The collection of service descriptors, with the JWT Bearer configured.</returns>
        public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services)
        {
            return services.ConfigureOptions<ConfigureJwtBearerOptions>()
                           .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer();
        }
    }
}