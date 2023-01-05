namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
using SynergyISP.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Humanizer;

internal abstract class EntityTypeConfigurationBase<TEntity, TKey>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : Id
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        SetEntityName(builder);
        BuildColumns(builder);
        BuildRelations(builder);
        BuildIndexes(builder);
    }

    public virtual void BuildColumns(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnType("uuid");
    }

    public abstract void BuildRelations(EntityTypeBuilder<TEntity> builder);

    public abstract void BuildIndexes(EntityTypeBuilder<TEntity> builder);

    public virtual void SetEntityName(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name.Pluralize());
    }
}
