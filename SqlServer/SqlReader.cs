using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PowerDataNet.SqlServer
{
    public sealed class SqlReader
    {
        public static int DEFAULT_SQL_TIMEOUT = 60;
        ISqlConnectionStringProvider _connectionStringProvider;

        public SqlReader(ISqlConnectionStringProvider sqlConnectionStringProvider)
        {
            _connectionStringProvider = sqlConnectionStringProvider ?? throw new ArgumentNullException(nameof(sqlConnectionStringProvider));
        }

        public List<T> SqlExecuteReader<T>(string query, Action<SqlCommand> sqlCommand, Func<SqlDataReader, T> map)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = DEFAULT_SQL_TIMEOUT;

                sqlCommand?.Invoke(command);

                List<T> elements = null;

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        elements = new List<T>();

                    while (reader.Read())
                    {
                        elements.Add(map(reader));
                    }
                }

                return elements;
            }
        }

        public List<T> SqlExecuteReader<T>(string query, Func<SqlDataReader, T> map)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = DEFAULT_SQL_TIMEOUT;

                List<T> elements = null;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        elements = new List<T>();

                    while (reader.Read())
                    {
                        elements.Add(map(reader));
                    }
                }

                return elements;
            }
        }

        private SqlConnection GetConnection()
        {
            var con = new SqlConnection(_connectionStringProvider.GetConnectionString());
            con.Open();

            return con;
        }
    }
}
