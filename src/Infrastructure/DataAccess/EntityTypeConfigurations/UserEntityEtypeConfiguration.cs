namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;

using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

public class UserEntityTypeConfiguration : AuditableEntityEntityTypeConfigurationBase<User<UserId>, UserId>
{
    private readonly IHostEnvironment _hostEnvironment;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserEntityTypeConfiguration"/> class.
    /// </summary>
    /// <param name="hostEnvironment">The host environment.</param>
    //public UserEntityEtypeConfiguration(IHostEnvironment hostEnvironment)
    //{
    //    _hostEnvironment = hostEnvironment;
    //}

    public override void Configure(EntityTypeBuilder<User<UserId>> builder)
    {
        base.Configure(builder);
        //if (_hostEnvironment.IsDevelopment())
        //{
        //    builder.HasData(SynergyInitialData.GetUsers());
        //}
    }

    /// <inheritdoc/>
    public override void BuildColumns(EntityTypeBuilder<User<UserId>> builder)
    {
        base.BuildColumns(builder);
        builder
            .Property(e => e.Id)
            .HasConversion(
                v => Guid.Parse(v.ToString()),
                v => new UserId(v))
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()");
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
    public override void BuildIndexes(EntityTypeBuilder<User<UserId>> builder)
    {
        builder.HasIndex(e => e.UserName).IsUnique();
    }

    /// <inheritdoc/>
    public override void BuildRelations(EntityTypeBuilder<User<UserId>> builder)
    {
    }
}