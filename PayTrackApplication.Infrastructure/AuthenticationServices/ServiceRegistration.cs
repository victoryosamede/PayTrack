using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PayTrackApplication.Application.Services.AuthenticationServices;

namespace PayTrackApplication.Infrastructure.AuthenticationServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, TokenConfiguration tokenConfiguration)
        {
            services.AddScoped<IAuthenticationManager>(o => new AuthenticationManager(tokenConfiguration));
            services.AddTokenValidation(tokenConfiguration.SecretKey);
            return services;
        }

        private static IServiceCollection AddTokenValidation(this IServiceCollection services, string key)
        {
            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            return services;
        }
    }
}
