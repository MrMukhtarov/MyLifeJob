using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL.Repositories.Implements;

public class TextRepository : Repository<Text>, ITextRepository
{
    public TextRepository(AppDbContext context) : base(context)
    {
    }
}
