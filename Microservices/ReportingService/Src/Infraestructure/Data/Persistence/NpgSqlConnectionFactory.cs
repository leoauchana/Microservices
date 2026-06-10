using System.Data;
using Application.Interfaces.Repositories;
using Infraestructure.Data.Configurations;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Infraestructure.Data.Persistence;

public class NpgSqlConnectionFactory : IDbConnectionFactory
{
    private readonly DatabaseOptions _databaseOptions;
    public NpgSqlConnectionFactory(
        IOptions<DatabaseOptions> databaseOptions)
    {
        _databaseOptions = databaseOptions.Value;
    }
    public async Task<IDbConnection> CreateConnection()
    {
        var connection =new NpgsqlConnection(_databaseOptions.Db_Report);
        
        await connection.OpenAsync();

        return connection;
    }
}
