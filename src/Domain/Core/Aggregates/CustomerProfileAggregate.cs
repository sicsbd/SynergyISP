
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Aggregates;
/// <summary>
/// The user profile aggregate.
/// </summary>
public record class CustomerProfileAggregate
    : UserProfileAggregate<Customer, CustomerId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerProfileAggregate"/> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    public CustomerProfileAggregate()
    {
    }
}
