using Core.Options;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<T>(databaseSettings.Value.CollectionName);
        }

        public void Add(T item)
        {
            _collection.InsertOne(item);
        }

        public T? FindById(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public List<T> GetAllAsList()
        {
            return _collection.Find(_ => true).ToList();
        }

        
    }
}
