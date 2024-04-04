using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

namespace Todo.Infrastructure.Persistence;

public class DapperDbContext
{
    private readonly string _connectionString;

    public DapperDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public IDbConnection CreateConnection()
        => new SQLiteConnection(_connectionString);
}