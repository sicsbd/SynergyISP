using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynergyISP.Domain.Abstractions;

namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
public abstract class AuditableEntityEntityTypeConfigurationBase<TEntity, TKey>
    : EntityTypeConfigurationBase<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>, IEntity<TKey>
    where TKey : Id
{
    /// <summary>
    /// Builds the columns.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void BuildColumns(EntityTypeBuilder<TEntity> builder)
    {
        base.BuildColumns(builder);
        builder.Property(e => e.LastModifiedAt).IsRowVersion();
        builder.HasQueryFilter(e => !e.IsDeleted ?? true);
    }
}
