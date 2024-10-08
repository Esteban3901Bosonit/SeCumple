namespace SeCumple.Infrastructure.Persistence.Interfaces;

public interface IGenericRepository<T>
{
    Task<IEnumerable<TResponse>> ExecuteQueryAsync<T, TResponse>(string queryName, T? parameters = default);
    Task<int> ExecuteCommandAsync<T>(string queryName, T? parameters = default);
    Task<IEnumerable<T>> GetAllAsync<T>();
    Task<T?> GetByIdAsync<T>(int id);
    Task<int> AddAsync<T>(T entity);
    Task<int> UpdateAsync<T>(T entity);
    Task<int> DeleteAsync<T>(int id);

    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync<T>(int pageNumber,
        int pageSize,
        string sortProperty,
        bool sortAscending);
}