using Microsoft.Extensions.DependencyInjection;

namespace SeCumple.CrossCutting;

public static class ExtensionServices
{
    public static IServiceCollection AddCrossCuttingDependencies(this IServiceCollection services)
    {
        return services;
    }
}