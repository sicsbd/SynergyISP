namespace SynergyISP.Domain.Abstractions.Events;

public interface IHasDomainEvent<TEvent, TPayload>
    : IHasEvent<TEvent, TPayload>
    where TEvent : DomainEvent<TPayload>
{
}
