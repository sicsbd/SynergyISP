namespace SynergyISP.Domain.Abstractions.Events;

using Entities;
using ValueObjects;

public abstract record class UserAddedEvent<TUser, TKey>
    : DomainEvent<TUser>
    where TUser : User<TKey>, new()
    where TKey : UserId, new()
{

}
