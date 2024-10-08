using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeCumple.CrossCutting.Options;
using SeCumple.Infrastructure.Persistence.Context;
using SeCumple.Infrastructure.Persistence.Interfaces;
using SeCumple.Infrastructure.Persistence.Repositories;

namespace SeCumple.Infrastructure;

public static class ExtensionServices
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services = AddContext(services);
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        return services;
    }

    private static IServiceCollection AddContext(this IServiceCollection services)
    {
        var assembly = typeof(SeCumpleDbContext).Assembly.FullName;
        services.AddDbContext<SeCumpleDbContext>((provider, o) =>
        {
            var settingOptions = GetSettingOptions(provider).Value;
            o.UseSqlServer(settingOptions.ConnectionString,
                y => y.MigrationsAssembly(assembly));
        }, ServiceLifetime.Transient);

        return services;
    }

    private static IOptions<SettingOptions> GetSettingOptions(IServiceProvider provider)
    {
        return provider.GetRequiredService<IOptions<SettingOptions>>();
    }
}