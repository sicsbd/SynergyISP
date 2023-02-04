using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SynergyISP.Application.Common.Mappings;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Application;
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
        var mapper = conf.CreateMapper();
        services.AddSingleton(_ => mapper.ConfigurationProvider);
        services.AddSingleton(_ => mapper);
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
