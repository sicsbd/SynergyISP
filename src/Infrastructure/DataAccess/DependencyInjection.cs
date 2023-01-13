namespace SynergyISP.Infrastructure;

using DataAccess;
using DataAccess.Repositories;
using Domain.Abstractions;
using Domain.ValueObjects;
using JasperFx.CodeGeneration;
using Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Weasel.Core;
using static Marten.MartenServiceCollectionExtensions;

internal static class DependencyInjection
{
    /// <summary>
    /// Adds the data access.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="hostEnvironment">The host environment.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="connectionStringName">The connection string name.</param>
    /// <param name="noSqlConnectionStringName">The no sql connection string name.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        string connectionStringName,
        string noSqlConnectionStringName)
    {
        services.AddRelationalDb(hostEnvironment, configuration, connectionStringName);
        services.AddNonRelationalDb(hostEnvironment, configuration, noSqlConnectionStringName);
        services.AddTransient(typeof(IReadRepository<,,>), typeof(ReadRepository<,,>));
        return services;
    }

    /// <summary>
    /// Adds the non relational db.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="hostEnvironment">The host environment.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="noSqlConnectionStringName">The no sql connection string name.</param>
    /// <returns>An MartenConfigurationExpression.</returns>
    private static MartenConfigurationExpression AddNonRelationalDb(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        string noSqlConnectionStringName)
    {
        bool isDevelopment = hostEnvironment.IsDevelopment();
        var martenSvcCollection = services.AddMarten(opt =>
        {
            opt.DatabaseSchemaName = "Synergy";
            opt.Schema.Include<SynergyRegistry>();
            opt.UseDefaultSerialization(
                enumStorage: EnumStorage.AsString,
                casing: Casing.CamelCase);
            opt.Connection(configuration.GetConnectionString(noSqlConnectionStringName)!);
        })
        .OptimizeArtifactWorkflow(isDevelopment ? TypeLoadMode.Dynamic : TypeLoadMode.Static)
        .UseLightweightSessions();
        if (isDevelopment)
        {
            martenSvcCollection.InitializeWith<SynergyInitialData>();
            martenSvcCollection.ApplyAllDatabaseChangesOnStartup();
        }

        return martenSvcCollection;
    }

    private static IServiceCollection AddRelationalDb(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        string connectionStringName)
    {
        bool isDevelopment = hostEnvironment.IsDevelopment();
        services.AddPooledDbContextFactory<DataContext>(dbCtxOpts =>
        {
            DatabaseSettings dbSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;
            var connectionString = configuration.GetConnectionString(connectionStringName);
            dbCtxOpts.UseNpgsql(connectionString, pgDbCtxOpts =>
            {
                pgDbCtxOpts.CommandTimeout(dbSettings.CommandTimeout ?? 0);

                // pgDbCtxOpts.EnableRetryOnFailure(dbSettings.MaxRetryCount, dbSettings.MaxRetryDelay, null);
                pgDbCtxOpts.UseRedshift(dbSettings.UseRedShift ?? false);
                pgDbCtxOpts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            dbCtxOpts.ConfigureWarnings(b => b.Default(WarningBehavior.Log));
            dbCtxOpts.EnableDetailedErrors(isDevelopment);
            dbCtxOpts.EnableSensitiveDataLogging(isDevelopment);
            dbCtxOpts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddTransient<IReadDataContext>(ResolveDataContext);
        services.AddTransient<IWriteDataContext>(ResolveDataContext);
        //NpgsqlConnection.GlobalTypeMapper.MapComposite<Id>("uuid");
        //NpgsqlConnection.GlobalTypeMapper.MapComposite<UserId>("uuid");
        //NpgsqlConnection.GlobalTypeMapper.MapComposite<Name>("nvarchar(50)");
        //NpgsqlConnection.GlobalTypeMapper.MapComposite<UserName>("nvarchar(100)");
        //NpgsqlConnection.GlobalTypeMapper.MapComposite<Password>("nvarchar(100)");
        return services;
    }

    private static DataContext ResolveDataContext(IServiceProvider sp)
    {
        IDbContextFactory<DataContext> ctxFactory = sp.GetRequiredService<IDbContextFactory<DataContext>>();
        return ctxFactory.CreateDbContext();
    }
}
