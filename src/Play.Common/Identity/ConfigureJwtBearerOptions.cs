using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Play.Common.Settings.Extensions;

namespace Play.Common.Identity
{
    /// <summary>
    /// Configures JWT Bearer options for authentication.
    /// </summary>
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureJwtBearerOptions"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration properties.</param>
        public ConfigureJwtBearerOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures the JWT Bearer options.
        /// </summary>
        /// <param name="name">Authentication scheme name.</param>
        /// <param name="options">The Jwt Bearer options.</param>
        public void Configure(string? name, JwtBearerOptions options)
        {
            if (name == JwtBearerDefaults.AuthenticationScheme)
            {
                var serviceSettings = _configuration.GetServiceSettings();

                options.Authority = serviceSettings.Authority;
                options.Audience = serviceSettings.ServiceName;
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            }
        }

        /// <summary>
        /// Configures the JWT Bearer options.
        /// </summary>
        /// <param name="options">The Jwt Bearer options.</param>
        public void Configure(JwtBearerOptions options)
        {
            Configure(Options.DefaultName, options);
        }
    }
}