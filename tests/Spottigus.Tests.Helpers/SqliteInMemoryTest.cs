using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace Spottigus.Tests.Helpers
{
    public class SqliteInMemoryTest : IDisposable
    {
        protected DbConnection _connection;

        protected DbConnection CreateInMemoryDatabase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            return _connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}