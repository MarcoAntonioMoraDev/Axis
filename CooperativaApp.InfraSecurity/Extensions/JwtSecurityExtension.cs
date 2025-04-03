using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.InfraSecurity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CooperativaApp.InfraSecurity.Extensions
{
    public static class JwtSecurityExtension
    {
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });


            services.AddTransient<IUsuarioSecurity, UsuarioSecurity>();
            return services;
        }
    }
}
