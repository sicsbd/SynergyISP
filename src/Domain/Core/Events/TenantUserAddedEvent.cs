namespace SynergyISP.Domain.Events;

using Entities;
using SynergyISP.Domain.Abstractions.Events;
using ValueObjects;

public sealed record class TenantUserAddedEvent
    : UserAddedEvent<TenantUser, TenantUserId>
{
}
