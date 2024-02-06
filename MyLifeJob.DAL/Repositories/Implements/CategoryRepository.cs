using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL.Repositories.Implements;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
