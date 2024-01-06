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
    }
}
