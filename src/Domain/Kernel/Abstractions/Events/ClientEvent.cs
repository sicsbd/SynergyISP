namespace SynergyISP.Domain.Abstractions.Events;

public abstract record class ClientEvent<TPayload>
    : AbstractEvent<TPayload>
{
}
