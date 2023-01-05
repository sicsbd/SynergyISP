namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
using SynergyISP.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

internal abstract class AuditableEntityEntityTypeConfigurationBase<TEntity, TKey>
    : EntityTypeConfigurationBase<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>, IEntity<TKey>
    where TKey : Id
{
    public override void BuildColumns(EntityTypeBuilder<TEntity> builder)
    {
        base.BuildColumns(builder);
        builder.Property(e => e.LastModifiedAt).IsRowVersion();
    }
}
