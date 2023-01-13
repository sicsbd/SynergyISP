namespace SynergyISP.Domain.Aggregates;

using Abstractions;
using SynergyISP.Domain.Entities;
using ValueObjects;

/// <summary>
/// The user profile aggregate.
/// </summary>
public interface IUserProfileAggregate<TUser, TKey>
    : IAggregate
    where TUser : User<TKey>
    where TKey : UserId
{
    public IUserProfileAggregate<TUser, TKey> ChangeProfile(UserId userId, string key, string value);
}
