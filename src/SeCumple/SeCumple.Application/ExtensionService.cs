using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SeCumple.Application.Behaviors;
using SeCumple.Application.Components.Documents.Commands.CreateDocument;
using SeCumple.Application.Interfaces;
using SeCumple.Application.Services;

namespace SeCumple.Application;

public static class ExtensionServices
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IFileManagement, FileManagement>();
    
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


    return services;
    }
}