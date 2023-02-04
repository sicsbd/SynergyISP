
using SynergyISP.Domain.Abstractions.Events;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Events;
public sealed record class OrganizationUserAddedEvent
    : UserAddedEvent<OrganizationUser, OrganizationUserId>
{
}
