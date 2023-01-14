namespace SynergyISP.Domain.Events;

using Entities;
using SynergyISP.Domain.Abstractions.Events;
using ValueObjects;

public sealed record class CustomerAddedEvent
    : UserAddedEvent<Customer, CustomerId>
{
}
