using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SeCumple.Application;

public static class ExtensionServices
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}