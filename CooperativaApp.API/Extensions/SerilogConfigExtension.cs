using Serilog.Sinks.Elasticsearch;
using Serilog;

namespace CooperativaApp.API.Extensions
{

    public static class SerilogConfigExtension
    {
        public static WebApplicationBuilder AddSeriLogConfigExtension(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext().Enrich.WithMachineName()
                .Enrich.WithThreadId().Enrich.WithProcessId()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}")
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                { AutoRegisterTemplate = true, IndexFormat = "logs-elk-api-{0:yyyy.MM.dd}" })
                .CreateLogger();

            builder.Host.UseSerilog();
            return builder;
        }
        public static WebApplication UseSeriLogConfigExtension(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
            app.MapGet("/log", () =>
            {
                var logger = Log.ForContext<Program>(); logger.Information("Teste de Log INFO"); logger.Warning("Teste de Log WARNING"); logger.Error("Teste de Log ERROR");
                return "Log gerados!";
            });
            return app;
        }
    }
}

