using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeCumple.CrossCutting.Options;

namespace SeCumple.Infrastructure.Persistence.Context;

public class DapperContext(IOptions<SettingOptions> options, ILogger<DapperContext> logger)
{
    
    public IDbConnection GetConnection()
    {
        var connection = new SqlConnection(options.Value.ConnectionString);
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        return connection;
    }

    public static void CloseConnection(IDbConnection connection)
    {
        if (connection.State is ConnectionState.Open or ConnectionState.Broken)
        {
            connection.Close();
        }
    }

    public string Query(string name)
    {
        try
        {
            var lines = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Sql/{name}.sql"));
            return lines;
        }
        catch (Exception e)
        {
            var message = $"{name}.sql was not found. Error: {e.Message}";
            logger.LogError(message, e);
            throw;
        }
    }
}