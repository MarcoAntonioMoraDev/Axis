using Microsoft.OpenApi.Models;

namespace CooperativaApp.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cooperativa API",
                    Version = "v1",
                    Description = "API para gerenciamento de Cooperativa.",
                    Contact = new OpenApiContact
                    {
                        Name = "Marco Antonio DEV",
                        Email = "marcoantonio.74ro@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/m4rco-4ntonio/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // Configuração do botão para envio do token JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu_token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cooperativa API v1");
                c.DocumentTitle = "Cooperativa API Docs";
            });

            return app;
        }
    }
}
