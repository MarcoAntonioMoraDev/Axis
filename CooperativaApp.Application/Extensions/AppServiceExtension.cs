using CooperativaApp.Application.Interfaces.Services;
using CooperativaApp.Application.Mappings;
using CooperativaApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CooperativaApp.Application.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CooperativaMapper), typeof(CooperadoMapper), typeof(ContatoFavoritoMapper), typeof(UsuarioMapper));

            services.AddTransient<ICooperativaService, CooperativaService>();
            services.AddTransient<ICooperadoService, CooperadoService>();
            services.AddTransient<IContatoFavoritoService, ContatoFavoritoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}
