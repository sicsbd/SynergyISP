using System.Text.Json.Serialization;
using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Entities;
public sealed record class Customer
    : User<CustomerId>, IUserAggregateRoot<Customer, CustomerId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Customer"/> class.
    /// </summary>
    [JsonConstructor]
    public Customer()
    {
        Id = new CustomerId();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Customer"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Customer(CustomerId id)
        : base(id)
    {
    }

    /// <inheritdoc />
    public new List<CustomerProfileAggregate>? Profile { get; init; }

    /// <inheritdoc/>
    public new IUserAggregateRoot<Customer, CustomerId> ChangeAccount(
        CustomerId? id,
        UserName? userName,
        Name? firstName,
        Name? lastName,
        Name? displayName,
        Name? nickName,
        Password? password,
        List<CustomerProfileAggregate>? profile)
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

    /// <inheritdoc />
    Task<IUserAggregateRoot<Customer, CustomerId>> IUserAggregateRoot<Customer, CustomerId>.CreateAsync()
    {
        return Task.FromResult(this as IUserAggregateRoot<Customer, CustomerId>);
    }

    public override Task<IUserAggregateRoot<User<CustomerId>, CustomerId>> CreateAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public new Customer ToEntity()
    {
        return this;
    }

    /// <inheritdoc/>
    IUserAggregateRoot<Customer, CustomerId> IUserAggregateRoot<Customer, CustomerId>.ChangePassword(Password password)
    {
        return this with { Password = password };
    }

    /// <inheritdoc/>
    IUserAggregate<Customer, CustomerId> IUserAggregate<Customer, CustomerId>.ChangeProfile(List<UserProfileAggregate<Customer, CustomerId>> profile)
    {
        return this with
        {
            Profile = profile.Select(x => x as CustomerProfileAggregate).ToList() !,
        };
    }
}
