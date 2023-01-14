namespace SynergyISP.Domain.Entities;

using System;
using SynergyISP.Domain.Aggregates;
using ValueObjects;

public record class CustomerProfile : IUserProfileAggregate<Customer, CustomerId>
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

    /// <inheritdoc/>
    public IUserProfileAggregate<Customer, CustomerId> ChangeProfile(CustomerId userId, string key, string value)
    {
        return this with
        {
            Id = userId,
            ProfileKey = key,
            DataType = null!,
            Value = value
        };
    }

    /// <inheritdoc/>
    public void ResolveDependencies(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }
}
