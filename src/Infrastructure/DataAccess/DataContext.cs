namespace SynergyISP.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.ValueObjects;
using SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;

/// <summary>
/// The data context.
/// </summary>
public class DataContext
    : DbContext, IReadDataContext, IWriteDataContext
{
    private readonly IHostEnvironment _hostEnvironment;

    public DataContext(
        DbContextOptions<DataContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        modelBuilder.HasPostgresExtension("uuid-ossp");
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc/>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder
            .DefaultTypeMapping<Id>()
            .HasColumnType("uuid")
            .HasConversion(typeof(Guid));
        configurationBuilder
            .DefaultTypeMapping<UserId>()
            .HasColumnType("uuid")
            .HasConversion(typeof(Guid));
        configurationBuilder
            .DefaultTypeMapping<Name>()
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .HasConversion(typeof(string));
        configurationBuilder
            .DefaultTypeMapping<UserName>()
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .HasConversion(typeof(string));
        configurationBuilder
            .DefaultTypeMapping<Password>()
            .HasColumnType("nvarchar")
            .HasMaxLength(500)
            .HasConversion(typeof(string));
    }
}
