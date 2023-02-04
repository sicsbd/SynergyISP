using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Aggregates;
/// <summary>
/// The user aggregate root.
/// </summary>
/// <typeparam name="TUser">The user type.</typeparam>
/// <typeparam name="TKey">Thek Key type for the user.</typeparam>
public interface IUserAggregateRoot<TUser, TKey>
    : IAggregateRoot<TUser, TKey>, IUserAggregate<TUser, TKey>
    where TUser : User<TKey>, IAggregateRoot<TUser, TKey>
    where TKey : UserId
{
    /// <summary>
    /// Changes the password.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>An IUserAggregateRoot.</returns>
    IUserAggregateRoot<TUser, TKey>? ChangePassword(Password password);

    /// <summary>
    /// Changes the account.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="nickName">The nick name.</param>
    /// <param name="password">The password.</param>
    /// <param name="profile">The profile.</param>
    /// <returns>An IUserAggregateRoot.</returns>
    IUserAggregateRoot<User<TKey>, TKey> ChangeAccount(
        TKey? id,
        UserName? userName,
        Name? firstName,
        Name? lastName,
        Name? displayName,
        Name? nickName,
        Password? password,
        List<UserProfileAggregate<User<TKey>, TKey>>? profile);

    /// <summary>
    /// Creates the user into the database asynchronously.
    /// </summary>
    /// <returns>the aggregate root.</returns>
    Task<IUserAggregateRoot<TUser, TKey>> CreateAsync();
}
