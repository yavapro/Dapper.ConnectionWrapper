namespace Dapper.ConnectionWrapper
{
    using Microsoft.Extensions.DependencyInjection;
    
    public static class ServiceCollectionExtensions
    {
        public static void AddSqlDapperWrapper(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionProvider>(_ => new SqlConnectionProvider(connectionString));
            services.AddSingleton<IDbCommandExecutor, DapperCommandExecutor>();
        }
    }
}