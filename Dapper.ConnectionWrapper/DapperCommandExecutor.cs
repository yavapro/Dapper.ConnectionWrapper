namespace Dapper.ConnectionWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;
    
    public class DapperCommandExecutor : IDbCommandExecutor
    {
        private const int MaxParametersStringLength = 1024;

        /// <summary>
        /// Execute parameterized SQL
        /// </summary>
        /// <returns>
        /// Number of rows affected
        /// </returns>
        public int Execute(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);
                
                try
                {
                    return connection.Execute(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Execute parameterized SQL asynchronously  
        /// </summary>
        /// <returns>
        /// Number of rows affected
        /// </returns>
        public async Task<int> ExecuteAsync(
            IDbConnectionProvider dbConnectionProvider,
            string commandText, 
            object parameters = null, 
            CommandType? commandType = CommandType.StoredProcedure, 
            IDbTransaction transaction = null, 
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);
                
                try
                {
                    return await connection.ExecuteAsync(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value
        /// </summary>
        /// <returns>
        /// The first cell selected
        /// </returns>
        public T ExecuteScalar<T>(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);
                
                try
                {
                    return connection.ExecuteScalar<T>(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value
        /// </summary>
        /// <returns>
        /// The first cell selected
        /// </returns>
        public async Task<T> ExecuteScalarAsync<T>(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);
                
                try
                {
                    return await connection.ExecuteScalarAsync<T>(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <returns>
        /// A sequence of data of the supplied type.
        /// </returns>
        public IEnumerable<T> Query<T>(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);

                try
                {
                    return connection.Query<T>(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <returns>
        /// A sequence of data of the supplied type.
        /// </returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);
                
                try
                {
                    return await connection.QueryAsync<T>(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Execute a command that returns multiple result sets
        /// </summary>
        public void QueryMultiple(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            Action<SqlMapper.GridReader> readDataAction,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);

                SqlMapper.GridReader gridReader;

                try
                {
                    gridReader = connection.QueryMultiple(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }

                readDataAction(gridReader);
            }
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <returns>
        /// A first-row data or default value of the supplied type.
        /// </returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);
                
                try
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(
                        commandDefinition.CommandText,
                        commandDefinition.Parameters, 
                        commandDefinition.Transaction,
                        commandDefinition.CommandTimeout,
                        commandDefinition.CommandType);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <returns>
        /// A first row data or default value of the supplied type.
        /// </returns>
        public T QueryFirstOrDefault<T>(
            IDbConnectionProvider dbConnectionProvider,
            string commandText,
            object parameters = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (var connection = dbConnectionProvider.GetConnection())
            {
                var commandDefinition = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType);

                try
                {
                    return connection.QueryFirstOrDefault<T>(commandDefinition);
                }
                catch (DbException ex)
                {
                    throw CreateDbCommandException(ex, commandDefinition);
                }
            }
        }

        private Exception CreateDbCommandException(DbException exception, CommandDefinition commandDefinition)
        {
            string parameters;
            
            if (commandDefinition.Parameters != null)
            {
                parameters = commandDefinition.Parameters.ToString();
                
                if (parameters.Length > MaxParametersStringLength)
                {
                    parameters = parameters.Substring(0, MaxParametersStringLength);
                }
            }
            else
            {
                parameters = string.Empty;
            }

            var dbCommandException = new DbCommandException(exception)
            {
                CommandText = commandDefinition.CommandText,
                Parameters = parameters
            };

            return dbCommandException;
        }
    }
}