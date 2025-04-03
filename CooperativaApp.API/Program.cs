using CooperativaApp.Infra.Data.SqlServer.Extensions;
using CooperativaApp.API.Extensions;
using CooperativaApp.Application.Extensions;
using CooperativaApp.Domain.Extensions;
using CooperativaApp.InfraSecurity.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

ConfigureSerilog(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerConfig();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddDomainServices();
builder.Services.AddJwtSecurity(builder.Configuration);
builder.AddSeriLogConfigExtension();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSeriLogConfigExtension();
app.UseExceptionHandler("/error"); // Global exception handler
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Global exception handling middleware - Improved error response
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Unhandled exception");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." }); //More user-friendly
    }
});


// Separate method for Serilog configuration (Improved Readability)
static void ConfigureSerilog(IConfiguration configuration)
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}


public partial class Program { }