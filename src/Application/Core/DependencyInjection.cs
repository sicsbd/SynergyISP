using Microsoft.Extensions.DependencyInjection;

namespace SynergyISP.Application;
public static class DependencyInjection
{
    /// <summary>
    /// Adds the application.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
