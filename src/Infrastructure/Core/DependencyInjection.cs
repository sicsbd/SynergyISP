namespace SynergyISP.Infrastructure;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        string connectionStringName,
        string noSqlConnectionStringName)
    {
        services.AddDataAccess(
            hostEnvironment,
            configuration,
            connectionStringName,
            noSqlConnectionStringName);
        return services;
    }
}
