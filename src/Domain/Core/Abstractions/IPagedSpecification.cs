namespace SynergyISP.Domain.Abstractions;

public interface IPagedSpecification<TEntity, TKey>
    : ISpecification<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : Id
{
    public int CurrentPage { get; }
    public int ItemsPerPage { get; }
}
