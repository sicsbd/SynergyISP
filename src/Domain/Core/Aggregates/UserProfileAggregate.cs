namespace SynergyISP.Domain.Aggregates;

using System;
using Entities;
using ValueObjects;

/// <summary>
/// The user profile aggregate.
/// </summary>
public record class UserProfileAggregate
    : IUserProfileAggregate<User<UserId>, UserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfileAggregate"/> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    public UserProfileAggregate()
    {
    }

    /// <inheritdoc/>
    public string ProfileKey { get; private init; }

    /// <inheritdoc/>
    public string DataType { get; private init; }

    /// <inheritdoc/>
    public string Value { get; private init; }

    /// <inheritdoc/>
    public IUserProfileAggregate<User<UserId>, UserId> ChangeProfile(UserId userId, string key, string value)
    {
        return this with
        {
            ProfileKey = key,
            DataType = null!,
            Value = value,
        };
    }

    /// <inheritdoc/>
    public void ResolveDependencies(IServiceProvider serviceProvider)
    {
    }
}
