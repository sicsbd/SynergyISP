using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Entities;
public sealed record class TenantUser
    : User<TenantUserId>, IAggregateRoot<TenantUser, TenantUserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUser"/> class.
    /// </summary>
    public TenantUser()
    {
        Id = new ();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUser"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public TenantUser(TenantUserId id)
        : base(id)
    {
    }

    /// <inheritdoc />
    public override Task<IUserAggregateRoot<User<TenantUserId>, TenantUserId>> CreateAsync()
    {
        return Task.FromResult(this as IUserAggregateRoot<User<TenantUserId>, TenantUserId>);
    }

    /// <inheritdoc />
    public new TenantUser ToEntity()
    {
        return this;
    }
}
