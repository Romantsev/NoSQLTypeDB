using System.Linq.Expressions;

namespace BLL.Interface;

public interface IGenericService<T>
{
    void Add(T item);
    T? GetById(int id);
    List<T> GetByPredicate(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null);
}