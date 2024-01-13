using Microsoft.EntityFrameworkCore;
using MyLifeJob.Core.Entity.Commons;
using System.Linq.Expressions;

namespace MyLifeJob.DAL.Repositories.Implements;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    public DbSet<T> Table => throw new NotImplementedException();

    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<T> FindByIdAsync(int id, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAllAsync(params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public void RevertSoftDelete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public void SofDelete(T entity)
    {
        throw new NotImplementedException();
    }
}
