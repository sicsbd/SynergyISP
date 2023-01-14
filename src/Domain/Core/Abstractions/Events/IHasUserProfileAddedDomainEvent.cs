namespace SynergyISP.Domain.Abstractions.Events;
using Entities;

public interface IHasUserProfileAddedDomainEvent
    : IHasDomainEvent<DomainEvent<CustomerProfile>, CustomerProfile>
{
}