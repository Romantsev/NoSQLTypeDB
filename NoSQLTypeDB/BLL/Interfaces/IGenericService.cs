using MongoDB.Bson;
using System.Linq.Expressions;

namespace BLL.Interface;

public interface IGenericService<T>
{
    void Add(T item);
    List<T> GetByPredicate(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null);
    void DeleteById(ObjectId id);
    public void UpdateById(ObjectId id, BsonDocument document);
}