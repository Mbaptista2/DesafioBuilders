using BST.Data;
using LiteDB;
using System;
using System.Linq;

namespace BST
{
    public class Repository<T> where T : IModel
    {
        private readonly string _connection;

        public Repository(string connection)
        {
            _connection = connection;
        }

        public void Insert(T classe)
        {
            try
            {
                using (var db = new LiteDatabase(_connection))
                {
                    var col = db.GetCollection<T>(typeof(T).Name);
                    col.Insert(classe);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T Find(Guid guid)
        {
            using (var db = new LiteDatabase(_connection))
            {
                var col = db.GetCollection<T>(typeof(T).Name);
                var result = col.Find(x => x.Id == guid);
                return result.FirstOrDefault();
            }
        }
    }
}