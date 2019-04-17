namespace Dapper.ConnectionWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Newtonsoft.Json;

    public class MockCommandExecutor : IDbCommandExecutor
    {
        private readonly IList<string> sqlResult;

        public MockCommandExecutor(List<string> sqlResult = null)
        {
            this.sqlResult = sqlResult ?? new List<string>();
        }

        public virtual int Execute(IDbConnectionProvider dbConnectionProvider, string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return sqlResult.Count;
        }

        public virtual Task<int> ExecuteAsync(IDbConnectionProvider dbConnectionProvider, string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Task.FromResult(sqlResult.Count);
        }

        public virtual T ExecuteScalar<T>(IDbConnectionProvider dbConnectionProvider, string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return JsonConvert.DeserializeObject<T>(sqlResult.FirstOrDefault());
        }

        public virtual Task<T> ExecuteScalarAsync<T>(IDbConnectionProvider dbConnectionProvider, string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<T>(sqlResult.FirstOrDefault()));
        }

        public virtual IEnumerable<T> Query<T>(IDbConnectionProvider dbConnectionProvider, string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return sqlResult.Select(JsonConvert.DeserializeObject<T>).ToList();
        }

        public virtual T QueryFirstOrDefault<T>(IDbConnectionProvider dbConnectionProvider, string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return JsonConvert.DeserializeObject<T>(sqlResult.FirstOrDefault());
        }

        public virtual Task<IEnumerable<T>> QueryAsync<T>(IDbConnectionProvider dbConnectionProvider,
            string commandText, object parameters = null,
            CommandType? commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Task.FromResult<IEnumerable<T>>(sqlResult.Select(JsonConvert.DeserializeObject<T>).ToList());
        }

        public virtual Task<T> QueryFirstOrDefaultAsync<T>(IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null, CommandType? commandType = CommandType.Text, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<T>(sqlResult.FirstOrDefault()));
        }

        public virtual void QueryMultiple(IDbConnectionProvider dbConnectionProvider, string commandText,
            Action<object> readDataAction,
            object parameters = null, CommandType? commandType = CommandType.Text, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
        }
        
        public virtual Task QueryMultipleAsync(IDbConnectionProvider dbConnectionProvider, string commandText,
            Action<object> readDataAction,
            object parameters = null, CommandType? commandType = CommandType.Text, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return Task.Factory.StartNew(readDataAction, sqlResult);
        }
    }
}