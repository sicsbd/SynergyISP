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
public class OrganizationalUserEntityTypeConfiguration
    : UserEntityTypeConfiguration<OrganizationUser, OrganizationUserId>
{
    /// <inheritdoc/>
    public override void BuildColumns(EntityTypeBuilder<OrganizationUser> builder)
    {
        base.BuildColumns(builder);
        builder
            .Property(e => e.Id)
            .HasConversion(
                v => Guid.Parse(v.ToString()),
                v => new OrganizationUserId(v))
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()");
    }

    /// <inheritdoc/>
    public override void BuildIndexes(EntityTypeBuilder<OrganizationUser> builder)
    {
        builder.HasIndex(e => e.UserName).IsUnique();
    }

    /// <inheritdoc/>
    public override void BuildRelations(EntityTypeBuilder<OrganizationUser> builder)
    {
    }

    /// <summary>
    /// Populates the data.
    /// </summary>
    /// <returns>A list of OrganizationUsers.</returns>
    public override IEnumerable<OrganizationUser> PopulateData()
    {
        return SynergyInitialData.PopulateOrganizationUsers();
    }
}