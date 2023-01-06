namespace SynergyISP.Application;
using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;
using Common.Mappings;
using Domain.ValueObjects;
using MediatR;
using FluentValidation.AspNetCore;
using FluentValidation;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the application.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="assemblies">The assemblies.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMappings(assemblies);
        services.AddMediatR(assemblies);
        services.AddValidation(assemblies);
        return services;
    }

    internal static MapperConfiguration PopulateConfiguration(params Assembly[] assemblies)
    {
        return new MapperConfiguration(c => c.AddProfiles(new[] { new SynergyProfile(assemblies) }));
    }

    private static IServiceCollection AddMappings(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        Assembly[] scanAssemblies = assemblies.Concat(GetAssemblies()).ToArray();
        MapperConfiguration conf = PopulateConfiguration(scanAssemblies);
        services.AddSingleton(conf);
        services.AddSingleton<IConfigurationProvider, MapperConfigurationProvider>();
        services.AddSingleton(_ => conf.CreateMapper());
        return services;
    }

    private static Assembly[] GetAssemblies()
    {
        var domainAssembly = typeof(Domain.Entities.User<UserId>).Assembly;
        return new Assembly[]
        {
            typeof(DependencyInjection).Assembly,
            domainAssembly,
            Assembly.GetCallingAssembly(),
            Assembly.GetExecutingAssembly(),
        };
    }

    private static IServiceCollection AddValidation(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddFluentValidationAutoValidation(cfg
            => cfg.DisableDataAnnotationsValidation = true);

        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);
        return services;
    }

}
