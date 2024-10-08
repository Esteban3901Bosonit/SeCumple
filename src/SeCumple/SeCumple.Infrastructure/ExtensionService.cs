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
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>(provider =>
        {
            var settingOptions = provider.GetRequiredService<IOptions<SettingOptions>>();

            // Crear DapperContext usando la cadena de conexión de SettingOptions
            var logger = provider.GetRequiredService<ILogger<DapperContext>>();
            return new DapperContext(settingOptions, logger);
        });
        

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
}