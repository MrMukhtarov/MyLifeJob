using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL.Repositories.Implements;

public class RequirementRepository : Repository<Requirement>, IRequirementRepository
{
    public RequirementRepository(AppDbContext context) : base(context)
    {
    }
}
