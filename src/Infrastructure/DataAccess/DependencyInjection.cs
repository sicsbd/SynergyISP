namespace SynergyISP.Infrastructure;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SynergyISP.Infrastructure.DataAccess;

internal static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        string connectionStringName,
        IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        services.AddDbContext<DataContext>(dbCtxOpts =>
        {
            bool isDevelopment = hostEnvironment.IsDevelopment();
            DatabaseSettings dbSettings = databaseSettingsOptions.Value;
            var connectionString = configuration.GetConnectionString(connectionStringName);
            dbCtxOpts.UseNpgsql(connectionString, pgDbCtxOpts =>
            {
                pgDbCtxOpts.CommandTimeout(dbSettings.CommandTimeout);
                pgDbCtxOpts.EnableRetryOnFailure(dbSettings.MaxRetryCount, dbSettings.MaxRetryDelay, null);
                pgDbCtxOpts.UseRedshift(true);
                pgDbCtxOpts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            dbCtxOpts.ConfigureWarnings(b => b.Default(WarningBehavior.Log));
            dbCtxOpts.EnableDetailedErrors(isDevelopment);
            dbCtxOpts.EnableSensitiveDataLogging(isDevelopment);
        });
        return services;
    }
}

public class DatabaseSettings
{
    public int? CommandTimeout { get; set; }
    public int MaxRetryCount { get; set; }
    public int RetryDelay { get; set; }
    public TimeSpan MaxRetryDelay => TimeSpan.FromSeconds(RetryDelay);
}
