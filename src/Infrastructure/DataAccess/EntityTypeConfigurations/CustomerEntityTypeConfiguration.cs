using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
/// <summary>
/// The user entity type configuration.
/// </summary>
/// <typeparam name="TUser">TheUser entity type.</param>
/// <typeparam name="TKey">The User Key type.</typeparam>
public class CustomerEntityTypeConfiguration
    : UserEntityTypeConfiguration<Customer, CustomerId>
{
    /// <inheritdoc/>
    public override void BuildColumns(EntityTypeBuilder<Customer> builder)
    {
        base.BuildColumns(builder);
        builder
            .Property(e => e.Id)
            .HasConversion(
                v => Guid.Parse(v.ToString()),
                v => new CustomerId(v))
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()");
    }

    /// <inheritdoc/>
    public override void BuildRelations(EntityTypeBuilder<Customer> builder)
    {
    }

    /// <summary>
    /// Populates the data.
    /// </summary>
    /// <returns>A list of Customers.</returns>
    public override IEnumerable<Customer> PopulateData()
    {
        IEnumerable<Customer> data = SynergyInitialData.PopulateCustomers();
        SynergyInitialData.InitialCustomers = data;
        return data;
    }
}
