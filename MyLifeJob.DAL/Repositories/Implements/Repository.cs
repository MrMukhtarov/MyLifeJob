using Microsoft.EntityFrameworkCore;
using MyLifeJob.Core.Entity.Commons;
using MyLifeJob.DAL.Contexts;
using System.Linq.Expressions;

namespace MyLifeJob.DAL.Repositories.Implements;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{

    readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FindByIdAsync(id);
        _context.Remove(entity);
    }

    public IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = Table.AsQueryable();
        return _getIncludes(query, includes).Where(expression);
    }

    public async Task<T> FindByIdAsync(int id, params string[] includes)
    {
        if (includes.Length == 0)
        {
            return await Table.FindAsync(id);
        }
        var query = Table.AsQueryable();
        return await _getIncludes(query, includes).SingleOrDefaultAsync(t => t.Id == id);
    }

    public IQueryable<T> GetAllAsync(params string[] includes)
    {
        var query = Table.AsQueryable();
        return _getIncludes(query, includes);
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = Table.AsQueryable();
        return await _getIncludes(query, includes).SingleOrDefaultAsync(expression);
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        return await Table.AnyAsync(expression); 
    }

    public void RevertSoftDelete(T entity)
    {
        entity.IsDeleted = false;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void SofDelete(T entity)
    {
        entity.IsDeleted = true;
    }

    IQueryable<T> _getIncludes(IQueryable<T> query, params string[] includes)
    {
        foreach (var item in includes)
        {
            query = query.Include(item);
        }
        return query;
    }
}
