namespace SynergyISP.Domain.Aggregates;

using System;
using ValueObjects;

/// <summary>
/// The user profile aggregate.
/// </summary>
internal record class UserProfileAggregate
    : IUserProfileAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfileAggregate"/> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    public UserProfileAggregate(Name firstName)
    {
        FirstName = firstName;
    }

    /// <summary>
    /// Gets or inits the first name.
    /// </summary>
    public Name FirstName { get; init; }

    /// <summary>
    /// Gets or inits the last name.
    /// </summary>
    public Name? LastName { get; init; }

    /// <summary>
    /// Gets or inits the display name.
    /// </summary>
    public Name? DisplayName { get; init; }

    /// <summary>
    /// Gets or inits the nick name.
    /// </summary>
    public Name? NickName { get; init; }

    /// <inheritdoc/>
    public void ResolveDependencies(IServiceProvider serviceProvider)
    {
    }
}
