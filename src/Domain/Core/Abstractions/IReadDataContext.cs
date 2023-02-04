using Microsoft.EntityFrameworkCore;

namespace SynergyISP.Domain.Abstractions;
/// <summary>
/// The data context for read operations.
/// </summary>
public interface IReadDataContext : IDisposable, IAsyncDisposable
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}
