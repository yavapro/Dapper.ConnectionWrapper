namespace Dapper.ConnectionWrapper
{
    using System.Data;
    using System.Data.SqlClient;
    
    public class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string sqlConnectionString;

        public SqlConnectionProvider(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }
        
        public IDbConnection GetConnection()
        {
            return new SqlConnection(sqlConnectionString);
        }
    }
}