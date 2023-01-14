namespace SynergyISP.Domain.Abstractions.Events;
public abstract record class AbstractEvent<TPayload>
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public TPayload Payload { get; init; }
}
