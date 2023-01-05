namespace SynergyISP.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

/// <summary>
/// The data context for write operations.
/// </summary>
public interface IWriteDataContext
{
    ChangeTracker ChangeTracker { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);

    Task<int> SaveChangesAsync(CancellationToken cancellation = default);

    Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellation = default);
}
