namespace Dapper.ConnectionWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    
    public interface IDbCommandExecutor
    {
        int Execute(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<int> ExecuteAsync(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        T ExecuteScalar<T>(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<T> ExecuteScalarAsync<T>(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        IEnumerable<T> Query<T>(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        T QueryFirstOrDefault<T>(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<IEnumerable<T>> QueryAsync<T>(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<T> QueryFirstOrDefaultAsync<T>(IDbConnectionProvider dbConnectionProvider, string commandText, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);

        void QueryMultiple(IDbConnectionProvider dbConnectionProvider, string commandText, Action<SqlMapper.GridReader> readDataAction, object parameters = null, CommandType? commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, int? commandTimeout = null);
    }
}