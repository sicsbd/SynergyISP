namespace SynergyISP.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The data context for read operations.
/// </summary>
public interface IReadDataContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}
