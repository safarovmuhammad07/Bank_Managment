using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Infratructure.DataContext;


public class DapperContext 
{
    private readonly string connectionString =
        "Server=localhost; Port = 5432; Database = test; User Id = postgres; Password = 1234;";

    public IDbConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
