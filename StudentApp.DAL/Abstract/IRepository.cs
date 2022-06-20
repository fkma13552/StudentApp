namespace StudentApp.DAL.Abstract;

public interface IRepository<T, K> 
    where T : class 
    where K : struct 
{
    Task<T> Create(T entity);
    Task<T> GetById(K id);
    void Delete(K id);
    T Update(T entity);
    void SaveChanges();
}