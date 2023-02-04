
using SynergyISP.Domain.Abstractions.Events;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Events;
public sealed record class TenantUserAddedEvent
    : UserAddedEvent<TenantUser, TenantUserId>
{
}
