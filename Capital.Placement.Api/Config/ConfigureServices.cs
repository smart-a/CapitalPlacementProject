using System.Reflection;
using Capital.Placement.Api.Interfaces;
using Capital.Placement.Api.Services;

namespace Capital.Placement.Api.Config;

public static class ConfigureServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CosmosDbSetting>(
            config.GetSection("CosmosDb"));
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IProgramService, ProgramService>();
        
        return services;
    }
}