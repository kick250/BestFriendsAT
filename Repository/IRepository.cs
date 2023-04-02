using Infrastructure.Exceptions;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Repository;

public abstract class IRepository
{
    private string? StringConnection { get; set ; }

    public IRepository(IConfiguration configuration)
    {
        StringConnection = configuration.GetConnectionString("ApplicationDbConnectionString");
    }

    protected SqlCommand CreateCommand(string query)
    {
        SqlCommand command = OpenConnection().CreateCommand();
        command.CommandText = query;
        command.CommandType = CommandType.Text;

        return command;
    }

    protected SqlParameter CreateParameter(string name, SqlDbType type, dynamic? value)
    {
        var param = new SqlParameter(name, type);
        param.Value = value;

        return param;
    }

    protected SqlConnection OpenConnection()
    {
        if (StringConnection == null)
            throw new StringConnectionEmptyException();

        var connection = new SqlConnection(StringConnection);
        connection.Open();

        return connection;
    }
}
