namespace SynergyISP.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SynergyISP.Domain.Abstractions;

/// <summary>
/// The data context.
/// </summary>
internal class DataContext
    : DbContext, IReadDataContext, IWriteDataContext
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
