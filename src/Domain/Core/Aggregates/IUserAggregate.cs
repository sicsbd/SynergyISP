using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Aggregates;
/// <summary>
/// The user aggregate.
/// </summary>
public interface IUserAggregate<TUser, TKey>
    : IAggregate
    where TUser : User<TKey>
    where TKey : UserId
{
    /// <summary>
    /// Changes the profile.
    /// </summary>
    /// <param name="profile">The profile.</param>
    /// <returns>An IUserAggregate.</returns>
    public IUserAggregate<TUser, TKey> ChangeProfile(List<UserProfileAggregate<TUser, TKey>> profile);
}
