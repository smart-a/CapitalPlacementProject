using Capital.Placement.Api.Config;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Integration.Test;

public class DbFixture : IDisposable
{
    private readonly CosmosClient _client;
    private readonly Container _container;
    public readonly IOptions<CosmosDbSetting> DbOption;
    private bool _dispose = false;
    
    public DbFixture(string container)
    {
        var (services, config) = Setup.Init();
        var serviceProvider = services.BuildServiceProvider();
        DbOption = serviceProvider.GetRequiredService<IOptions<CosmosDbSetting>>();
        
        _client = new CosmosClient(DbOption.Value.Account, DbOption.Value.Key);
        var database = InitDb();
        
        _container = database
            .CreateContainerIfNotExistsAsync(container, "/id")
            .GetAwaiter()
            .GetResult()
            .Container;
    }

    public Database InitDb() => _client.CreateDatabaseIfNotExistsAsync(DbOption.Value.DatabaseName)
        .GetAwaiter().GetResult().Database;
    
    public async Task InsertAsync<TEntity>(string partitionKey, TEntity entity) where TEntity : class 
        => await _container.CreateItemAsync(entity, new PartitionKey(partitionKey));

    public async Task GetEntity<TEntity>(string expectedId, TaskCompletionSource<TEntity> receivedTask)
    {
        try
        {
            var entity = (await _container
                .ReadItemAsync<TEntity>(expectedId, new PartitionKey(expectedId)))
                .Resource;

            receivedTask.TrySetResult(entity);
        }
        catch (CosmosException) //For handling item not found and other exceptions
        {
            receivedTask.TrySetCanceled();
        }
    }
    
    // public async Task<TEntity?> GetEntity<TEntity>(string expectedId)
    // {
    //     try
    //     {
    //         return (await _container
    //                 .ReadItemAsync<TEntity>(expectedId, new PartitionKey(expectedId)))
    //             .Resource;
    //     }
    //     catch (CosmosException) //For handling item not found and other exceptions
    //     {
    //         return null;
    //     }
    // }

    protected virtual void Dispose(bool disposing)
    {
        if (_dispose)
        {
            return;
        }

        if (disposing)
        {
             _client.GetDatabase(DbOption.Value.DatabaseName)
                 .DeleteAsync()
                 .GetAwaiter()
                 .GetResult();
        }

        _dispose = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}