using SynergyISP.Domain.Entities;
using SynergyISP.Domain.Events;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Abstractions.Events;
public interface IHasUserAddedDomainEvent<TEvent, TUser, TKey>
    : IHasDomainEvent<TEvent, TUser>
    where TEvent : DomainEvent<TUser>
    where TUser : User<TKey>, new()
    where TKey : UserId, new()
{
}

public interface IHasCustomerAddedDomainEvent
    : IHasDomainEvent<CustomerAddedEvent, Customer>
{
}
