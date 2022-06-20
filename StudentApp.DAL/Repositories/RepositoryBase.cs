using Microsoft.EntityFrameworkCore;
using StudentApp.DAL.Abstract;

namespace StudentApp.DAL.Repositories;

public class RepositoryBase<T, K> : IRepository<T, K> 
    where T : class
    where K : struct
{
    private readonly StudentContext _context;
    protected readonly DbSet<T> dbSet;

    public RepositoryBase(StudentContext context)
    {
        _context = context;
        dbSet = context.Set<T>();
    }
    public async Task<T> Create(T entity)
    {
        var entry = await dbSet.AddAsync(entity).ConfigureAwait(false);
        return entry.Entity;
    }

    public async Task<T> GetById(K id)
    {
        return await dbSet.FindAsync(id);
    }

    public async void Delete(K id)
    {
        var entity = await dbSet.FindAsync(id).ConfigureAwait(false);
        if (entity is not null)
        {
            dbSet.Remove(entity);
        }
    }

    public T Update(T entity)
    {
        return dbSet.Update(entity).Entity;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}