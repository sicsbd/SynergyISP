namespace SynergyISP.Domain.Aggregates;
using Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

/// <summary>
/// The user aggregate.
/// </summary>

public interface IUserAggregate<TUser, TKey>
    : IAggregate
    where TUser : User<TKey>
    where TKey : UserId
{
    /// <summary>
    /// Gets the profile.
    /// </summary>
    IUserProfileAggregate<TUser, TKey> Profile { get; }

    /// <summary>
    /// Changes the profile.
    /// </summary>
    /// <param name="profile">The profile.</param>
    /// <returns>An IUserAggregate.</returns>
    public IUserAggregate<TUser, TKey> ChangeProfile(IUserProfileAggregate<TUser, TKey> profile);
}
