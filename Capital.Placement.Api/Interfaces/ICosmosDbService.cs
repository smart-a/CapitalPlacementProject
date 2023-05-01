namespace Capital.Placement.Api.Interfaces;

public interface ICosmosDbService<TModel> where TModel : class
{
    Task<IEnumerable<TMappedOut>> GetMultipleAsync<TMappedOut>(string query);
    Task<TModel?> GetAsync(string id);
    Task AddAsync(TModel item, string partitionKey);
    Task UpdateAsync(string id, TModel item);
    Task DeleteAsync(string id);
}