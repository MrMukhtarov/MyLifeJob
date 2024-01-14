using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL.Repositories.Implements;

public class IndustryRepository : Repository<Industry>, IIndustiryRepository
{
    public IndustryRepository(AppDbContext context) : base(context)
    {
    }
}
