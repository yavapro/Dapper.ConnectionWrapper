namespace Dapper.ConnectionWrapper.Tests.Unit
{
    using System;
    using System.Data.SqlClient;
    using Xunit;

    public class SqlConnectionProviderTests
    {
        private string connectionString = "Server=test;";

        [Fact]
        public void CreateObjectTest()
        {
            var provider = new SqlConnectionProvider(connectionString);
            Assert.NotNull(provider);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenConnectionStringNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlConnectionProvider(null));
        }

        [Fact]
        public void ThrowArgumentExceptionWhenConnectionStringEmptyTest()
        {
            Assert.Throws<ArgumentException>(() => new SqlConnectionProvider(string.Empty));
        }

        [Fact]
        public void ThrowArgumentExceptionWhenConnectionStringOnlyWithWhiteSpacesTest()
        {
            Assert.Throws<ArgumentException>(() => new SqlConnectionProvider(" "));
        }

        [Fact]
        public void GetConnectionTest()
        {
            var provider = new SqlConnectionProvider(connectionString);
            Assert.NotNull(provider);

            var connection = provider.GetConnection();
            Assert.NotNull(connection);
            Assert.Equal(connectionString, connection.ConnectionString);
            Assert.IsType<SqlConnection>(connection);
        }

        [Fact]
        public void GetConnection_WithTemplate_Test()
        {
            var connectionStr = "Server={0};";
            var arg = "test";

            var provider = new SqlConnectionProvider(connectionStr);
            Assert.NotNull(provider);

            var connection = provider.GetConnection(arg);
            Assert.NotNull(connection);
            Assert.Equal(String.Format(connectionStr, arg), connection.ConnectionString);
            Assert.IsType<SqlConnection>(connection);
        }
    }
}