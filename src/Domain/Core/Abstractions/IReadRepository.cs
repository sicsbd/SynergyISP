namespace SynergyISP.Domain.Abstractions;
public interface IReadRepository<TEntity, TKey, TAggregateRoot>
    where TEntity : class, IEntity<TKey>, IAggregateRoot<TEntity, TKey>
    where TKey : Id
    where TAggregateRoot : IAggregateRoot<TEntity, TKey>
{
    #region Get
    TAggregateRoot? Get(TKey key);
    TAggregateRoot? Get(ISpecification<TEntity, TKey> specification);
    TAggregateRoot? GetSingle(TKey key);
    TAggregateRoot? GetSingle(ISpecification<TEntity, TKey> specification);
    #endregion

    #region GetAsync
    Task<TAggregateRoot?> GetAsync(TKey key, CancellationToken cancellationToken = default);
    Task<TAggregateRoot?> GetSingleAsync(TKey key, CancellationToken cancellationToken = default);
    Task<TAggregateRoot?> GetAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    Task<TAggregateRoot?> GetSingleAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    #endregion

    #region GetAll
    IQueryable<TAggregateRoot?> GetAll();
    IQueryable<TAggregateRoot?> GetAll(ISpecification<TEntity, TKey> specification);
    IQueryable<TReturnType?> GetAll<TReturnType>();
    IQueryable<TReturnType?> GetAll<TReturnType>(ISpecification<TEntity, TKey> specification);
    #endregion
}
