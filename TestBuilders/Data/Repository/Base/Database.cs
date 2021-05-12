using Data.Interfaces;

using MongoDB.Driver;

namespace Data.Repository.Base
{
    internal class Database<T> where T : IEntity
    {
       
        /// <summary>
        /// Creates and returns a MongoCollection from the connectionstring name and collection name
        /// </summary>
        /// <param name="connectionString">The connectionstring to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        internal static IMongoCollection<T> GetCollectionFromConnectionString(string connectionString, string databasename, string collectionName)
        {
            return GetCollectionFromUrl(new MongoUrl(connectionString), databasename, collectionName);
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        internal static IMongoCollection<T> GetCollectionFromUrl(MongoUrl url, string databaseName, string collectionName)
        {
            return GetDatabaseFromUrl(url, databaseName).GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Creates and returns a MongoDatabase from the specified url.
        /// </summary>
        /// <param name="url">The url to use to get the database from.</param>
        /// <returns>Returns a MongoDatabase from the specified url.</returns>
        private static IMongoDatabase GetDatabaseFromUrl(MongoUrl url, string databaseName)
        {
            var client = new MongoClient(url);
            return client.GetDatabase(databaseName); 
        }
    }
}