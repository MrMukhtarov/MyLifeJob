using Microsoft.Extensions.DependencyInjection;
using MyLifeJob.Business.Services.Implements;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.Business;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IAppUserService, AppUserService>();
    }
}
