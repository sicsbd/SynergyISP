namespace SynergyISP.Domain.Entities;

using Abstractions;
using SynergyISP.Domain.Aggregates;
using ValueObjects;

public record class User<TKey>
    : AuditableEntity<TKey>, IUserAggregateRoot
    where TKey : UserId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User{TKey}"/> class.
    /// </summary>
    /// <param name="id">User id.</param>
    public User(TKey id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets a value for user name.
    /// </summary>
    public UserName UserName { get; private init; } = string.Empty;

    /// <summary>
    /// Gets a value for first name.
    /// </summary>
    public Name FirstName { get; private init; } = string.Empty;

    /// <summary>
    /// Gets a value for last name.
    /// </summary>
    public Name? LastName { get; private init; } = string.Empty;

    /// <summary>
    /// Gets a value for display name.
    /// </summary>
    public Name? DisplayName { get; private init; }

    /// <summary>
    /// Gets a value for nick name.
    /// </summary>
    public Name? NickName { get; private init; }

    /// <summary>
    /// Gets the password.
    /// </summary>
    public Password Password { get; private init; }

    /// <inheritdoc/>
    public IUserProfileAggregate Profile
    {
        get => new UserProfileAggregate(FirstName)
        {
            LastName = LastName,
            DisplayName = DisplayName,
            NickName = NickName,
        };
        init
        {
            FirstName = value.FirstName;
            LastName = value.LastName;
            DisplayName = value.DisplayName;
            NickName = value.NickName;
        }
    }

    /// <inheritdoc/>
    public IUserAggregateRoot ChangeAccount(UserName? userName, Password? password, IUserProfileAggregate? profile)
    {
        return this with
        {
            UserName = userName ?? throw new InvalidOperationException("User name can not be null"),
            Password = password ?? throw new InvalidOperationException("Password can not be null"),
            FirstName = profile?.FirstName ?? throw new InvalidOperationException("First Name can not be null"),
            LastName = profile?.LastName ?? string.Empty,
            DisplayName = profile?.DisplayName ?? string.Empty,
            NickName = profile?.NickName ?? string.Empty,
        };
    }

    /// <inheritdoc/>
    public IUserAggregateRoot ChangePassword(Password password)
    {
        return this with { Password = password };
    }

    /// <inheritdoc/>
    public IUserAggregate ChangeProfile(IUserProfileAggregate profile)
    {
        return this with
        {
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            DisplayName = profile.DisplayName,
            NickName = profile.NickName,
        };
    }
}
