using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SynergyISP.Infrastructure;
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
