using AutoMapper;
using Capital.Placement.Api.Config;
using Capital.Placement.Api.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Capital.Placement.Api.Services;

public class CosmosDbService<TModel> : ICosmosDbService<TModel> where TModel : class
{
    private readonly IMapper _mapper;
    private readonly Container _container;

    public CosmosDbService(string container, IOptions<CosmosDbSetting> options, IMapper mapper)
    {
        _mapper = mapper;
        var client = new CosmosClient(options.Value.Account, options.Value.Key);
        var database = client.CreateDatabaseIfNotExistsAsync(options.Value.DatabaseName)
            .GetAwaiter().GetResult();
        
        _container = database.Database
            .CreateContainerIfNotExistsAsync(container, "/id")
            .GetAwaiter()
            .GetResult()
            .Container;
    }

    public async Task<IEnumerable<TMappedOut>> GetMultipleAsync<TMappedOut> 
        (string queryString = "SELECT * FROM c" )
    {
        var query = _container.GetItemQueryIterator<TModel>(new QueryDefinition(queryString));
        var results = new List<TMappedOut>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource.Select(o => 
                _mapper.Map<TMappedOut>(o)));
        }
        return results;
    }

    public async Task<TModel?> GetAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<TModel>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException) //For handling item not found and other exceptions
        {
            return null;
        }
    }

    public async Task AddAsync(TModel item, string partitionKey)
    {
        await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task UpdateAsync(string id, TModel item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(id));
    }

    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<TModel>(id, new PartitionKey(id));
    }
}