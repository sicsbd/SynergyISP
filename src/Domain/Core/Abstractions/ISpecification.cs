namespace SynergyISP.Domain.Abstractions;
using System.Linq.Expressions;
using SynergyISP.Domain.Enumns;

public interface ISpecification<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : Id
{
    public IReadOnlyList<Expression<Func<TEntity, bool>>> FilterClauses { get; }
    public IReadOnlyDictionary<Expression<Func<TEntity, object>>, SortOrder> OrderByClauses { get; }

    public IReadOnlyList<Expression<Func<TEntity, object>>> IncludeClauses { get; }
}
