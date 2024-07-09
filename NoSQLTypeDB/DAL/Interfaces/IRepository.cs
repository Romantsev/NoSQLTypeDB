using MongoDB.Bson;

namespace DAL.Interfaces;

public interface IRepository<T> where T : class
{
    List<T> GetAllAsList();
    void Add(T item);
    void DeleteById(ObjectId id);
    public void UpdateById(ObjectId id, BsonDocument document);
}