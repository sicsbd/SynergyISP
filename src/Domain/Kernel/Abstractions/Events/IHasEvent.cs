namespace SynergyISP.Domain.Abstractions.Events;

public interface IHasEvent<TEvent, TPayload>
    where TEvent : AbstractEvent<TPayload>
{
    IReadOnlyList<TEvent> Events { get; }

    void AddEvent(TEvent @event);

    void RemoveEvent(TEvent @event);
}
