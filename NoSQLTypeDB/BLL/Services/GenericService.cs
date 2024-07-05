using DAL.Interfaces;
using System.Linq.Expressions;
using BLL.Interface;
using MongoDB.Bson;

namespace BLL.Services;

public abstract class GenericService<T> : IGenericService<T> where T : class
{
    protected IRepository<T> _repository;

    protected GenericService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void Add(T item)
    {
        _repository.Add(item);
    }

    public List<T> GetByPredicate(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null)
    {
        var query = _repository.GetAllAsList().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (orderBy != null)
        {
            query = orderBy.Compile()(query);
        }
        return query.ToList();
    }

    public void DeleteById(ObjectId id)
    {
        _repository.DeleteById(id);
    }

    public void UpdateById(ObjectId id, BsonDocument document)
    {
        _repository.UpdateById(id, document);
    }
}