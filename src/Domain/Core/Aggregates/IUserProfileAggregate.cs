namespace SynergyISP.Domain.Aggregates;

using Abstractions;
using ValueObjects;

/// <summary>
/// The user profile aggregate.
/// </summary>
public interface IUserProfileAggregate : IAggregate
{
    /// <summary>
    /// Gets the first name.
    /// </summary>
    public Name FirstName { get; init; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public Name? LastName { get; init; }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public Name? DisplayName { get; init; }

    /// <summary>
    /// Gets the nick name.
    /// </summary>
    public Name? NickName { get; init; }
}
