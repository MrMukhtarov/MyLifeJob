using Microsoft.Extensions.DependencyInjection;
using MyLifeJob.DAL.Repositories.Implements;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL;

public static class RepositoryRegistration
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IIndustiryRepository, IndustryRepository>();
    }
}
