using Capital.Placement.Api.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Integration.Test;

public class Setup
{
    public static (ServiceCollection services, IConfiguration config) Init()
    {
        var services = new ServiceCollection();
        var settings = new CosmosDbSetting();
        
        var config = new ConfigurationBuilder()
            .SetBasePath( Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
        
        services.Configure<CosmosDbSetting>(config.GetSection("CosmosDb"));
        // var serviceProvider = services.BuildServiceProvider();
        // var opt = serviceProvider.GetRequiredService<IOptions<TestDbSetting>>().Value;

        return (services, config);
    }
}