namespace SynergyISP.Domain.Entities;

using System;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Aggregates;
using ValueObjects;

public record class UserProfile : IUserProfileAggregate<User<UserId>, UserId>
{
    /// <inheritdoc/>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or inits the profile key.
    /// </summary>
    public string ProfileKey { get; init; } = null!;

    /// <summary>
    /// Gets or inits the data type.
    /// </summary>
    public string DataType { get; init; } = null!;

    /// <summary>
    /// Gets or inits the value.
    /// </summary>
    public string Value { get; init; } = null!;

    public IUserProfileAggregate<User<UserId>, UserId> ChangeProfile(UserId userId, string key, string value)
    {
        return this with
        {
            Id = userId,
            ProfileKey = key,
            DataType = null!,
            Value = value
        };
    }

    public void ResolveDependencies(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }
}
