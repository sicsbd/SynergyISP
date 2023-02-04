using SynergyISP.Domain.Entities;

namespace SynergyISP.Domain.Abstractions.Events;
public interface IHasUserProfileAddedDomainEvent
    : IHasDomainEvent<DomainEvent<CustomerProfile>, CustomerProfile>
{
}