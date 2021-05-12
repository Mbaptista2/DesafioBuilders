using MongoDB.Driver;
using System;

namespace TestBST.Fixture
{
    public class DbFixture : IDisposable
    {
        public DbFixture()
        {
            this.connection = "mongodb://localhost:27017/";
            this.database = $"test_db_{Guid.NewGuid()}";
        }

        public string connection { get; }
        public string database { get; }

        public void Dispose()
        {
            var client = new MongoClient(connection);
            client.DropDatabase(database);
        }
    }
}