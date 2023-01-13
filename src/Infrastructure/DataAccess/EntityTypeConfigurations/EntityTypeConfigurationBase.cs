﻿namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
using Domain.Abstractions;
using Domain.ValueObjects;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class EntityTypeConfigurationBase<TEntity, TKey>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : Id
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        SetEntityName(builder);
        BuildColumns(builder);
        BuildRelations(builder);
        BuildIndexes(builder);
    }

    public virtual void BuildColumns(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasConversion(
                v => Guid.Parse(v.ToString()),
                v => (TKey)v)
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()");
    }

    public abstract void BuildRelations(EntityTypeBuilder<TEntity> builder);

    public abstract void BuildIndexes(EntityTypeBuilder<TEntity> builder);

    public virtual void SetEntityName(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name.Pluralize());
    }
}
