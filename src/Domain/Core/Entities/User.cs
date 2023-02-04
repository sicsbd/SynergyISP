using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Entities;
public abstract record class User<TKey>
    : AuditableEntity<TKey>, IUserAggregateRoot<User<TKey>, TKey>, IUserAggregate<User<TKey>, TKey>
    where TKey : UserId
{
    private IMediator? _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="User{TKey}}"/> class.
    /// </summary>
    protected User()
        : base((TKey)null!)
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

    /// <summary>
    /// Gets a value for user name.
    /// </summary>
    public UserName UserName { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets a value for first name.
    /// </summary>
    public Name FirstName { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets a value for last name.
    /// </summary>
    public Name? LastName { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets a value for display name.
    /// </summary>
    public Name? DisplayName { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets a value for nick name.
    /// </summary>
    public Name? NickName { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets the password.
    /// </summary>
    public Password Password { get; protected init; } = string.Empty;

    /// <inheritdoc/>
    public virtual List<UserProfileAggregate<User<TKey>, TKey>>? Profile { get; protected init; }

    /// <inheritdoc/>
    public virtual IUserAggregateRoot<User<TKey>, TKey> ChangeAccount(
        TKey? id,
        UserName? userName,
        Name? firstName,
        Name? lastName,
        Name? displayName,
        Name? nickName,
        Password? password,
        List<UserProfileAggregate<User<TKey>, TKey>>? profile)
    {
        return this with
        {
            Id = id ?? throw new ArgumentNullException(nameof(id)),
            UserName = userName ?? throw new InvalidOperationException("User name can not be null"),
            Password = password ?? throw new InvalidOperationException("Password can not be null"),
            FirstName = firstName ?? throw new InvalidOperationException("First name can not be null"),
            LastName = lastName,
            DisplayName = displayName,
            NickName = nickName,
            Profile = profile,
        };
    }

    /// <inheritdoc/>
    public virtual IUserAggregateRoot<User<TKey>, TKey> ChangePassword(Password password)
    {
        return this with { Password = password };
    }

    /// <inheritdoc/>
    public virtual IUserAggregate<User<TKey>, TKey> ChangeProfile(List<UserProfileAggregate<User<TKey>, TKey>> profile)
    {
        return this with
        {
            Profile = profile
        };
    }

    /// <inheritdoc/>
    public override void ResolveDependencies(IServiceProvider serviceProvider)
    {
        base.ResolveDependencies(serviceProvider);
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    /// <inheritdoc />
    public abstract Task<IUserAggregateRoot<User<TKey>, TKey>> CreateAsync();
}
