using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using CooperativaApp.Infra.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CooperativaApp.Infra.Data.SqlServer.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CooperativaAppConectionString");

            services.AddDbContext<CooperativaDataContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICooperativaRepository, CooperativaRepository>();
            services.AddScoped<ICooperadoRepository, CooperadoRepository>();
            services.AddScoped<IContatoFavoritoRepository, ContatoFavoritoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}