namespace Dapper.ConnectionWrapper
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection();

        IDbConnection GetConnection(params object[] args);
    }
}