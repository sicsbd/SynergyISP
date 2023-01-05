namespace SynergyISP.Domain.Aggregates;
using Abstractions;
/// <summary>
/// The user aggregate.
/// </summary>

public interface IUserAggregate : IAggregate
{
    /// <summary>
    /// Gets the profile.
    /// </summary>
    IUserProfileAggregate Profile { get; }

    /// <summary>
    /// Changes the profile.
    /// </summary>
    /// <param name="profile">The profile.</param>
    /// <returns>An IUserAggregate.</returns>
    public IUserAggregate ChangeProfile(IUserProfileAggregate profile);
}
