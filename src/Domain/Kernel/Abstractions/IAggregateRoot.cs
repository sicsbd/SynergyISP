namespace SynergyISP.Domain.Abstractions;

/// <summary>
/// The aggregate root.
/// </summary>
/// <typeparam name="TEntity">The entity.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IAggregateRoot<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, IAggregateRoot<TEntity, TKey>
    where TKey : Id
{
    /// <summary>
    /// Resolve all dependencies.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    void ResolveDependencies(IServiceProvider serviceProvider);

    /// <summary>
    /// Converts an aggregate root to entity.
    /// </summary>
    /// <returns>An entity.</returns>
    TEntity ToEntity()
    {
        return (this as TEntity)!;
    }
}
