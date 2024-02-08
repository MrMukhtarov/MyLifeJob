using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL.Repositories.Implements;

public class AdvertismentRepository : Repository<Advertisment>, IAdvertismentRepository
{
    public AdvertismentRepository(AppDbContext context) : base(context)
    {
    }
}
