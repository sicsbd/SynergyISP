namespace SynergyISP.Domain.Aggregates;
using Abstractions;
using Entities;
using ValueObjects;

/// <summary>
/// The user aggregate root.
/// </summary>
public interface IUserAggregateRoot : IAggregateRoot<User<UserId>, UserId>, IUserAggregate
{
    /// <summary>
    /// Changes the password.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>An IUserAggregateRoot.</returns>
    IUserAggregateRoot ChangePassword(Password password);

    /// <summary>
    /// Changes the account.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="password">The password.</param>
    /// <param name="profile">The profile.</param>
    /// <returns>An IUserAggregateRoot.</returns>
    IUserAggregateRoot ChangeAccount(UserName? userName, Password? password, IUserProfileAggregate? profile);
}
