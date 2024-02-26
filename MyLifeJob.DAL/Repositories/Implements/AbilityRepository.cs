using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL.Repositories.Implements;

public class AbilityRepository : Repository<Ability>, IAbilityRepository
{
    public AbilityRepository(AppDbContext context) : base(context)
    {
    }
}
