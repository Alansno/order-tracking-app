using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private IDbConnection _connection;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("Connection");
    }

    public IDbConnection GetConnection()
    {
        if (_connection == null || _connection.State == ConnectionState.Closed)
        {
            _connection = new SqlConnection(_connectionString);
        }

        return _connection;
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}