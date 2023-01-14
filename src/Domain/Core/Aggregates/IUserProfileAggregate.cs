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
    string ProfileKey { get; }
    string DataType { get; }
    string Value { get; }
    public IUserProfileAggregate<TUser, TKey> ChangeProfile(TKey userId, string key, string value);
}
