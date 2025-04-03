using CooperativaApp.Domain.Interfaces.Services;
using CooperativaApp.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CooperativaApp.Domain.Extensions
{
    public static class DomainServiceExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ICooperativaDomainService, CooperativaDomainService>();
            services.AddTransient<ICooperadoDomainService, CooperadoDomainService>();
            services.AddTransient<IContatoFavoritoDomainService, ContatoFavoritoDomainService>();
            services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();

            return services;
        }
    }
}