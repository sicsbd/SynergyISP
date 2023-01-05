namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

internal class UserEntityEtypeConfiguration : AuditableEntityEntityTypeConfigurationBase<User<UserId>, UserId>
{
    /// <inheritdoc/>
    public override void BuildColumns(EntityTypeBuilder<User<UserId>> builder)
    {
        base.BuildColumns(builder);
        builder.Property(e => e.UserName).HasColumnType("nvarchar(100)");
        builder.Property(e => e.FirstName).HasColumnType("nvarchar(50)");
        builder.Property(e => e.LastName).HasColumnType("nvarchar(50)");
        builder.Property(e => e.DisplayName).HasColumnType("nvarchar(50)");
        builder.Property(e => e.NickName).HasColumnType("nvarchar(50)");
    }

    /// <inheritdoc/>
    public override void BuildIndexes(EntityTypeBuilder<User<UserId>> builder)
    {
        builder.HasIndex(e => e.UserName).IsUnique();
    }

    /// <inheritdoc/>
    public override void BuildRelations(EntityTypeBuilder<User<UserId>> builder)
    {
    }
}