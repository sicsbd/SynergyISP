
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Aggregates;
/// <summary>
/// The user profile aggregate.
/// </summary>
public interface IUserProfileAggregate<TUser, TKey>
    : IAggregate
    where TUser : User<TKey>
    where TKey : UserId
{
    Guid Id { get; }
    string Field { get; }
    string DataType { get; }
    object Value { get; }
    public IUserProfileAggregate<TUser, TKey> ChangeInfo<T>(string key, T value, TKey? id = null)
        where T : notnull;
}
