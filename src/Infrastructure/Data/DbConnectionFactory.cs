using System.Data;
using Npgsql;

namespace Infrastructure.Data;

internal class DbConnectionFactory(NpgsqlDataSource dataSource)
{
    public IDbConnection OpenConnection()
    {
        NpgsqlConnection connection = dataSource.OpenConnection();

        return connection;
    }
}
