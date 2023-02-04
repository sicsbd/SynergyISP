
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Abstractions.Events;
public abstract record class UserAddedEvent<TUser, TKey>
    : DomainEvent<TUser>
    where TUser : User<TKey>, new()
    where TKey : UserId, new()
{

}
