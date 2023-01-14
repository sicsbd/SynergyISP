namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;

using System.Collections.Generic;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// The user entity type configuration.
/// </summary>
/// <typeparam name="TUser">TheUser entity type.</param>
/// <typeparam name="TKey">The User Key type.</typeparam>
public class TenantUserEntityTypeConfiguration
    : UserEntityTypeConfiguration<TenantUser, TenantUserId>
{
    /// <inheritdoc/>
    public override void BuildColumns(EntityTypeBuilder<TenantUser> builder)
    {
        base.BuildColumns(builder);
        builder
            .Property(e => e.Id)
            .HasConversion(
                v => Guid.Parse(v.ToString()),
                v => new TenantUserId(v))
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()");
    }

    /// <inheritdoc/>
    public override void BuildIndexes(EntityTypeBuilder<TenantUser> builder)
    {
        builder.HasIndex(e => e.UserName).IsUnique();
    }

    /// <inheritdoc/>
    public override void BuildRelations(EntityTypeBuilder<TenantUser> builder)
    {
    }

    /// <inheritdoc/>
    public override IEnumerable<TenantUser> PopulateData()
    {
        return SynergyInitialData.PopulateTenantUsers();
    }
}
