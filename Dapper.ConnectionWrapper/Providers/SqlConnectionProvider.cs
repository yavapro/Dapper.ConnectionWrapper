namespace Dapper.ConnectionWrapper
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string sqlConnectionString;

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

            this.sqlConnectionString = sqlConnectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(sqlConnectionString);
        }

        public IDbConnection GetConnection(params object[] args)
        {
            return new SqlConnection(string.Format(sqlConnectionString, args));
        }
    }
}