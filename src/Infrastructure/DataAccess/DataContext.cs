using Microsoft.EntityFrameworkCore;
using SynergyISP.Application.Common.Helpers;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Infrastructure.DataAccess;
/// <summary>
/// The data context.
/// </summary>
public class DataContext
    : DbContext, IReadDataContext, IWriteDataContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext"/> class.
    /// </summary>
    /// <param name="dbContextOptions">The db context options.</param>
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
        modelBuilder.HasPostgresExtension("fuzzystrmatch");
        modelBuilder.HasDbFunction(
            typeof(SoundsLikeHelper).GetMethods().Where(m => m.Name.Equals(nameof(SoundsLikeHelper.Soundex)) && m.GetParameters().Length == 1).First(),
            b =>
            {
                b.HasName("SOUNDEX");
                b.IsBuiltIn();
            });
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
