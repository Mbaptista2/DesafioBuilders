using Data.Interfaces;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace Data.Repository.Base
{
    public class Repository<T> : IRepository<T>
          where T : IEntity
    {
        #region MongoSpecific

        private IMongoCollection<T> _collection;
        private readonly string _connectionString;
        private readonly string _collectionName;
        private readonly string _databaseName;

        /// <summary>
        /// with custom settings
        /// </summary>
        /// <param name="connectionString">connection string</param>
        /// <param name="collectionName">collection name</param>
        public Repository(string connectionString, string dataBaseName, string collectionName)
        {
            _connectionString = connectionString;
            _databaseName = dataBaseName;
            _collectionName = collectionName;
        }

        private IMongoCollection<T> GetCollection()
        {
            return Database<T>.GetCollectionFromConnectionString(_connectionString, _databaseName, _collectionName);
        }

        /// <summary>
        /// mongo collection
        /// </summary>
        public IMongoCollection<T> Collection
        {
            get
            {
                if (_collection == null)
                    _collection = GetCollection();
                return _collection;
            }
        }

        /// <summary>
        /// filter for collection
        /// </summary>
        public FilterDefinitionBuilder<T> Filter
        {
            get
            {
                return Builders<T>.Filter;
            }
        }

        private IFindFluent<T, T> Query(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter);
        }

        #endregion MongoSpecific

        #region Find

        /// <summary>
        /// find entities
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>collection of entity</returns>
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return Query(filter).ToEnumerable();
        }

        #region Get

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id">id value</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public virtual T Get(string id)
        {
            return Retry(() =>
            {
                return Find(i => i.Id == id).FirstOrDefault();
            });
        }

        #endregion Get

        #region Insert

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual void Insert(T entity)
        {
            Retry(() =>
            {
                Collection.InsertOne(entity);
                return true;
            });
        }

        #endregion Insert

        #endregion Find

        #region RetryPolicy

        /// <summary>
        /// retry operation for three times if IOException occurs
        /// </summary>
        /// <typeparam name="TResult">return type</typeparam>
        /// <param name="action">action</param>
        /// <returns>action result</returns>
        /// <example>
        /// return Retry(() =>
        /// {
        ///     do_something;
        ///     return something;
        /// });
        /// </example>
        protected virtual TResult Retry<TResult>(Func<TResult> action)
        {
            return RetryPolicy
                .Handle<MongoConnectionException>(i => i.InnerException.GetType() == typeof(IOException) ||
                                                       i.InnerException.GetType() == typeof(SocketException))
                .Retry(3)
                .Execute(action);
        }

        #endregion RetryPolicy
    }
}