namespace SynergyISP.Domain.Aggregates;
using Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

/// <summary>
/// The user aggregate.
/// </summary>

public interface IUserAggregate : IAggregate
{
    /// <summary>
    /// Gets the profile.
    /// </summary>
    IUserProfileAggregate<User<UserId>, UserId> Profile { get; }

    /// <summary>
    /// Changes the profile.
    /// </summary>
    /// <param name="profile">The profile.</param>
    /// <returns>An IUserAggregate.</returns>
    public IUserAggregate ChangeProfile(IUserProfileAggregate<User<UserId>, UserId> profile);
}
