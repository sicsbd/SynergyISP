namespace SynergyISP.Domain.Abstractions.Events;
using Domain.ValueObjects;
using Entities;
using SynergyISP.Domain.Events;

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
