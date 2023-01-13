namespace SynergyISP.Domain.Entities;

using Abstractions;
using SynergyISP.Domain.Aggregates;
using ValueObjects;

public record class User<TKey>
    : AuditableEntity<TKey>, IUserAggregateRoot, IUserAggregate
    where TKey : UserId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    public User()
        : base((TKey)Guid.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User{TKey}"/> class.
    /// </summary>
    /// <param name="id">User id.</param>
    public User(TKey id)
        : base(id)
    {
    }

    /// <inheritdoc />
    public new TKey Id { get; private init; }

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
    public Name? DisplayName { get; private init; } = string.Empty;

    /// <summary>
    /// Gets a value for nick name.
    /// </summary>
    public Name? NickName { get; private init; } = string.Empty;

    /// <summary>
    /// Gets the password.
    /// </summary>
    public Password Password { get; private init; }

    /// <inheritdoc/>
    public IUserProfileAggregate<User<UserId>, UserId> Profile { get; private init; }

    /// <inheritdoc/>
    public IUserAggregateRoot ChangeAccount(
        UserName? userName,
        Name firstName,
        Name? lastName,
        Name? displayName,
        Name? nickName,
        Password? password,
        UserId? id,
        IUserProfileAggregate<User<UserId>, UserId>? profile)
    {
        return this with
        {
            Id = (TKey)(id ?? throw new ArgumentNullException(nameof(id))),
            UserName = userName ?? throw new InvalidOperationException("User name can not be null"),
            Password = password ?? throw new InvalidOperationException("Password can not be null"),
            FirstName = firstName,
            LastName = lastName,
            DisplayName = displayName,
            NickName = nickName,
        };
    }

    /// <inheritdoc/>
    public IUserAggregateRoot ChangePassword(Password password)
    {
        return this with { Password = password };
    }

    /// <inheritdoc/>
    public IUserAggregate ChangeProfile(IUserProfileAggregate<User<UserId>, UserId> profile)
    {
        return this;
    }
}
