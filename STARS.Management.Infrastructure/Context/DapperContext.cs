using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace STARS.Management.Infrastructure.Context;

public class DapperContext
{
       private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlDatabseConnection");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}