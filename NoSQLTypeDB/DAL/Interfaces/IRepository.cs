namespace DAL.Interfaces;

public interface IRepository<T> where T : class
{
    List<T> GetAllAsList();
    void Add(T item);
    T? FindById(int id);
   
}