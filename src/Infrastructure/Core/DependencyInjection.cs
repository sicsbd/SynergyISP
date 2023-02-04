
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SynergyISP.Infrastructure.Persistence;

namespace SynergyISP.Infrastructure;
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="hostEnvironment">The host environment.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="connectionStringName">The connection string name.</param>
    /// <param name="noSqlConnectionStringName">The no sql connection string name.</param>
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
        services.AddPersistence();
        return services;
    }
}
