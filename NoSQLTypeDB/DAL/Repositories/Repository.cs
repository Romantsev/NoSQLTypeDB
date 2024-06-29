using Core.Options;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(IOptions<DatabaseSettings> databaseSettings)
            private readonly IMongoCollection<Object> _objectCollection;
        {
            var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Book>(
                databaseSettings.Value.CollectionName);
        }
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public T? FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllAsList()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
