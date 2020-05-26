namespace Dapper.ConnectionWrapper
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection();

        IDbConnectionProvider FormatConnectionString(params object[] args);
    }
}