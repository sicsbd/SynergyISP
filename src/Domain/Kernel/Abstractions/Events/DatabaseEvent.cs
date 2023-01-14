namespace SynergyISP.Domain.Abstractions.Events;

public abstract record class DatabaseEvent<TPayload>
    : AbstractEvent<TPayload>
{
}
