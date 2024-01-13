using Microsoft.EntityFrameworkCore;
using MyLifeJob.Core.Entity.Commons;
using System.Linq.Expressions;

namespace MyLifeJob.DAL.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }
    IQueryable<T> GetAllAsync(params string[] includes);
    IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T> FindByIdAsync(int id, params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task SaveAsync();
    Task CreateAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(int id);
    void SofDelete(T entity);
    void RevertSoftDelete(T entity);    
}
