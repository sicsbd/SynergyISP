namespace SynergyISP.Domain.Abstractions.Events;
using MediatR;

public abstract record class DomainEvent<TPayload>
    : AbstractEvent<TPayload>, IRequest
{
}
