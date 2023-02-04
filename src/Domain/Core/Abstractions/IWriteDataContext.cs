using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SynergyISP.Domain.Abstractions;
/// <summary>
/// The data context for write operations.
/// </summary>
public interface IWriteDataContext : IDisposable, IAsyncDisposable
{
    ChangeTracker ChangeTracker { get; }
    DatabaseFacade Database { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);

    Task<int> SaveChangesAsync(CancellationToken cancellation = default);

    Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellation = default);
}
