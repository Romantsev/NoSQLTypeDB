using Core.Options;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public Repository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name);
    }

    public void Add(T item)
    {
        var document = item.ToBsonDocument();
        _collection.InsertOne(document);
    }

    public List<T> GetAllAsList()
    {
        var documents = _collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();
        return documents.Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
    }

    public void DeleteById(ObjectId id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        _collection.DeleteOne(filter);
    }

    public void UpdateById(ObjectId id, BsonDocument document)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        _collection.ReplaceOne(filter, document);
    }
}
