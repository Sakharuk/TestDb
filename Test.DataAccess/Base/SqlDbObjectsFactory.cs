using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess.Base
{
    public class SqlDbObjectsFactory : IDbObjectsFactory
    {
        public IDbConnection CreateConnection(string connectionString)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public IDbCommand CreateCommand(IDbConnection connection, string commandText, CommandType type, params object[] parameters)
        {
            IDbCommand dbCommand = new SqlCommand();
            dbCommand.Connection = connection;
            dbCommand.CommandType = type;
            dbCommand.CommandText = commandText;

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    dbCommand.Parameters.Add(p);
                }
            }

            return dbCommand;
        }
    }
}