namespace SynergyISP.Domain.Abstractions;
using System.Linq.Expressions;
public interface IReadRepository<TEntity, TKey, TAggregateRoot>
    where TEntity : class, IEntity<TKey>, IAggregateRoot<TEntity, TKey>
    where TKey : Id
    where TAggregateRoot : IAggregateRoot<TEntity, TKey>
{
    #region Get
    TEntity Get(TKey key);
    TEntity Get(ISpecification<TEntity, TKey> specification);
    TEntity GetSingle(TKey key);
    TEntity GetSingle(ISpecification<TEntity, TKey> specification);
    #endregion
    #region GetAsync
    Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default);
    Task<TEntity> GetSingleAsync(TKey key, CancellationToken cancellationToken = default);
    Task<TEntity> GetAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    Task<TEntity> GetSingleAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    #endregion

    IEnumerable<TAggregateRoot> GetAll();
    IAsyncEnumerable<TAggregateRoot> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IList<TAggregateRoot>> GetAllListAsync(CancellationToken cancellationToken = default);
    IEnumerable<TAggregateRoot> GetAll(ISpecification<TEntity, TKey> specification);
    IAsyncEnumerable<TAggregateRoot> GetAllAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    Task<IList<TAggregateRoot>> GetAllListAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    IEnumerable<TReturnType> GetAll<TReturnType>();
    IAsyncEnumerable<TReturnType> GetAllAsync<TReturnType>(CancellationToken cancellationToken = default);
    Task<IList<TReturnType>> GetAllListAsync<TReturnType>(CancellationToken cancellationToken = default);
    IEnumerable<TReturnType> GetAll<TReturnType>(ISpecification<TEntity, TKey> specification);
    IAsyncEnumerable<TReturnType> GetAllAsync<TReturnType>(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
    Task<IList<TReturnType>> GetAllListAsync<TReturnType>(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default);
}
