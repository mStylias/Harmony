using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Options;
using Todo.Domain.Options;

namespace Todo.Infrastructure.Persistence;

public class DapperDbContext
{
    private readonly string _connectionString;

    public DapperDbContext(IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _connectionString = connectionStringsOptions.Value.Default;
    }

    public IDbConnection CreateConnection()
        => new SQLiteConnection(_connectionString);
}