namespace Dapper.ConnectionWrapper
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string connectionString;

        public SqlConnectionProvider(string sqlConnectionString)
        {
            if (sqlConnectionString == null)
            {
                throw new ArgumentNullException(nameof(sqlConnectionString));
            }

            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                throw new ArgumentException(nameof(sqlConnectionString));
            }

            this.connectionString = sqlConnectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public IDbConnectionProvider FormatConnectionString(params object[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Length == 0)
            {
                throw new ArgumentException(nameof(args));
            }

            var connection = String.Format(connectionString, args);
            return new SqlConnectionProvider(connection);
        }
    }
}