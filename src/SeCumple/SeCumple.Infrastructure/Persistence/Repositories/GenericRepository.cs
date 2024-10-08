using System.Diagnostics;
using System.Text;
using Dapper;
using Microsoft.Extensions.Logging;
using SeCumple.CrossCutting.Attributes;
using SeCumple.Infrastructure.Persistence.Context;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(DapperContext context, ILogger<GenericRepository<T>> logger)
    : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<TResponse>> ExecuteQueryAsync<T, TResponse>(string queryName, T? parameters = default)
    {
        var query = context.Query(queryName);
        return await ExecuteWithLoggingAsync(queryName,
            async () => await context.GetConnection().QueryAsync<TResponse>(query, parameters)
        );
    }

    public async Task<int> ExecuteCommandAsync<T>(string queryName, T? parameters = default)
    {
        var query = context.Query(queryName);
        return await ExecuteWithLoggingAsync(queryName,
            async () => await context.GetConnection().ExecuteAsync(query, parameters)
        );
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>()
    {
        var tableName = GetTableName<T>();
        var properties = typeof(T).GetProperties();
        
        var columnNames = properties
            .Select(p => 
            {
                var columnName = GetColumnName<T>(p.Name); // Obtiene el nombre de la columna desde el atributo
                return !string.IsNullOrEmpty(columnName) 
                    ? $"{columnName} AS {p.Name}" // Retorna "NombreColumna AS NombrePropiedad"
                    : null; // Si columnName es vacío o nulo, no incluye nada
            })
            .Where(name => name != null);
        var columns = string.Join(", ", columnNames);
    
        var query = $"SELECT {columns} FROM {tableName}";
        
        return await ExecuteWithLoggingAsync(query,
            async () => await context.GetConnection().QueryAsync<T>(query)
        );
    }

    public async Task<T?> GetByIdAsync<T>(int id)
    {
        var tableName = GetTableName<T>();
        var properties = typeof(T).GetProperties();
        
        var columnNames = properties
            .Select(p => 
            {
                var columnName = GetColumnName<T>(p.Name); // Obtiene el nombre de la columna desde el atributo
                return !string.IsNullOrEmpty(columnName) 
                    ? $"{columnName} AS {p.Name}" // Retorna "NombreColumna AS NombrePropiedad"
                    : null; // Si columnName es vacío o nulo, no incluye nada
            })
            .Where(name => name != null);
        var columns = string.Join(", ", columnNames);
        
        var query = $"SELECT {columns} FROM {tableName} WHERE Id = @Id";
        return await ExecuteWithLoggingAsync(query,
            async () => await context.GetConnection().QueryFirstOrDefaultAsync<T>(query, new { Id = id })
        );
    }

    public async Task<int> AddAsync<T>(T entity)
    {
        var query = GenerateInsertQuery<T>();
        return await ExecuteWithLoggingAsync(query,
            async () => await context.GetConnection().ExecuteAsync(query, entity)
        );
    }

    public async Task<int> UpdateAsync<T>(T entity)
    {
        var query = GenerateUpdateQuery<T>();
        return await ExecuteWithLoggingAsync(query,
            async () => await context.GetConnection().ExecuteAsync(query, entity)
        );
    }

    public async Task<int> DeleteAsync<T>(int id)
    {
        var tableName = GetTableName<T>();
        var query = $"DELETE FROM {tableName} WHERE Id = @Id";
        return await ExecuteWithLoggingAsync(query,
            async () => await context.GetConnection().ExecuteAsync(query, new { Id = id })
        );
    }

    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync<T>(int pageNumber,
        int pageSize,
        string sortProperty = "Id",
        bool sortAscending = true)
    {
        var tableName = GetTableName<T>();
        var sortColumn = GetColumnName<T>(sortProperty);
        var sortOrder = sortAscending ? "ASC" : "DESC";

        var query = $@"
            SELECT * FROM {tableName}
            ORDER BY {sortColumn} {sortOrder}
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*) FROM {tableName}";

        return await ExecuteWithLoggingAsync(query,
            async () =>
            {
                var multi = await context.GetConnection().QueryMultipleAsync(query, new
                {
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                });

                var items = await multi.ReadAsync<T>();
                var totalCount = await multi.ReadFirstAsync<int>();

                return (items, totalCount);
            }
        );
    }

    private string GetTableName<T>()
    {
        var tableAttribute = typeof(T).GetCustomAttributes(false)
            .FirstOrDefault(x => x.GetType().Name == "TableAttribute") as TableAttribute;
        return tableAttribute?.Name ?? typeof(T).Name;
    }

    private string GetColumnName<T>(string propertyName)
    {
        var property = typeof(T).GetProperty(propertyName) ?? typeof(T).GetProperty("Id");

        // if (property == null)
        //     throw new ArgumentException($"Property {propertyName} does not exist on type {typeof(T).Name}");

        var columnAttribute = property!.GetCustomAttributes(false)
            .OfType<ColumnAttribute>()
            .FirstOrDefault();

        return columnAttribute?.Name!;
    }

    private string GenerateInsertQuery<T>()
    {
        var tableName = GetTableName<T>();
        var properties = typeof(T).GetProperties();
        var columnNames = properties.Select(p => GetColumnName<T>(p.Name));
        var parameterNames = properties.Select(p => "@" + p.Name);

        var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");
        insertQuery.Append('(');
        insertQuery.Append(string.Join(", ", columnNames));
        insertQuery.Append(") VALUES (");
        insertQuery.Append(string.Join(", ", parameterNames));
        insertQuery.Append(')');

        return insertQuery.ToString();
    }

    private string GenerateUpdateQuery<T>()
    {
        var tableName = GetTableName<T>();
        var properties = typeof(T).GetProperties();
        var setValues = properties.Select(p => $"{GetColumnName<T>(p.Name)} = @{p.Name}");
        const string idPropertyName = "Id";
        var idColumnName = GetColumnName<T>(idPropertyName);

        var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
        updateQuery.Append(string.Join(", ", setValues));
        updateQuery.Append($" WHERE {idColumnName} = @{idPropertyName}");

        return updateQuery.ToString();
    }

    private async Task<T> ExecuteWithLoggingAsync<T>(string queryName, Func<Task<T>> action)
    {
        try
        {
            logger.LogInformation($"Starting {queryName} operation in database.");
            var stopwatch = Stopwatch.StartNew();
            var result = await action();
            stopwatch.Stop();
            logger.LogInformation(
                $"{queryName} operation completed. Execution time: {stopwatch.ElapsedMilliseconds} ms");
            return result;
        }
        catch (Exception e)
        {
            var message = $"{queryName} was not executed. Error: {e.Message}";
            logger.LogError(message, e);
            throw; // new InfrastructureException(message);
        }
    }
}