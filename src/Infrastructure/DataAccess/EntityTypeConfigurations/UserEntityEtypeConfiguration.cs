namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;

using Comparers;
using Converters;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// The user entity type configuration.
/// </summary>
/// <typeparam name="TUser">TheUser entity type.</param>
/// <typeparam name="TKey">The User Key type.</typeparam>
public abstract class UserEntityTypeConfiguration<TUser, TKey>
    : AuditableEntityEntityTypeConfigurationBase<TUser, TKey>
    where TUser : User<TKey>
    where TKey : UserId
{
    /// <inheritdoc/>
    public override void BuildColumns(EntityTypeBuilder<TUser> builder)
    {
        base.BuildColumns(builder);
        builder
            .Property(e => e.UserName)
            .HasConversion(new UserNameConverter(), new UserNameComparer())
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder
            .Property(e => e.FirstName)
            .HasConversion(new NameConverter(), new NameComparer())
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);
        builder
            .Property(e => e.LastName)
            .HasConversion(new NameConverter(), new NameComparer())
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);
        builder
            .Property(e => e.DisplayName)
            .HasConversion(new NameConverter(), new NameComparer())
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);
        builder
            .Property(e => e.NickName)
            .HasConversion(new NameConverter(), new NameComparer())
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);
        builder
            .Property(e => e.Password)
            .HasConversion(
                v => v.ToString(),
                v => (Password)v)
            .HasColumnType("varchar(255)")
            .HasMaxLength(100);
        builder
            .Property(e => e.CreateDate)
            .HasDefaultValueSql("NOW()");
        builder
            .Property(e => e.CreatedBy)
            .HasConversion(new UserNameConverter(), new UserNameComparer())
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder
            .Property(e => e.LastModifiedBy)
            .HasConversion(new UserNameConverter(), new UserNameComparer())
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder
            .Property(e => e.DeletedBy)
            .HasConversion(new UserNameConverter(), new UserNameComparer())
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder.Ignore(e => e.Profile);
    }

    /// <inheritdoc/>
    public override void BuildIndexes(EntityTypeBuilder<TUser> builder)
    {
        builder.HasIndex(e => e.UserName).IsUnique();
    }

    /// <inheritdoc/>
    public override void BuildRelations(EntityTypeBuilder<TUser> builder)
    {
    }
}
