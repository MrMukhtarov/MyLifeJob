using Microsoft.Extensions.DependencyInjection;
using MyLifeJob.DAL.Repositories.Implements;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.DAL;

public static class RepositoryRegistration
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IIndustiryRepository, IndustryRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAdvertismentRepository, AdvertismentRepository>();
    }
}
