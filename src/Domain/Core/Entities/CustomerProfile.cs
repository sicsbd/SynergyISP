using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Entities;
public record class CustomerProfile
    : UserProfileAggregate<Customer, CustomerId>, IUserProfileAggregate<Customer, CustomerId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerProfile"/> class.
    /// </summary>
    public CustomerProfile()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerProfile"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public CustomerProfile(Guid id)
        : base()
    {
        Id = id;
    }
}
