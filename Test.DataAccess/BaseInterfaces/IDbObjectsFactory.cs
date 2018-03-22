using System.Data;

namespace Test.DataAccess.BaseInterfaces
{
    public interface IDbObjectsFactory
    {
        IDbCommand CreateCommand(IDbConnection connection, string commandText, CommandType type, params object[] parameters);
        IDbConnection CreateConnection(string connectionString);
    }
}
