using MediatR;

namespace SynergyISP.Domain.Abstractions.Events;
public abstract record class DomainEvent<TPayload>
    : AbstractEvent<TPayload>, IRequest
{
}
