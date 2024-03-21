using Microsoft.Extensions.DependencyInjection;
using MyLifeJob.Business.ExternalServices.Implements;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Implements;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.Business;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IAppUserService, AppUserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IIndustiryService, IndustryService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAdvertismentService, AdvertismentService>();
        services.AddScoped<IAbilityService, AbilityService>();
        services.AddScoped<ITextService, TextService>();
    }
}
