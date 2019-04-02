namespace Dapper.ConnectionWrapper
{
    using System;
    
    public class DbCommandException : Exception
    {
        private const string DefaultMessage = "An error occured while executing database command";

        public DbCommandException(Exception innerException, string message = DefaultMessage) : base(message, innerException)
        {
        }

        public string CommandText { get; set; }

        public string Parameters { get; set; }
    }
}