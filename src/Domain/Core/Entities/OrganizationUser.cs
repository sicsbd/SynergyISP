using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Entities;
public sealed record class OrganizationUser
    : User<OrganizationUserId>, IAggregateRoot<OrganizationUser, OrganizationUserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUser"/> class.
    /// </summary>
    public OrganizationUser()
    {
        Id = new ();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUser"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public OrganizationUser(OrganizationUserId id)
        : base(id)
    {
    }

    /// <inheritdoc />
    public override Task<IUserAggregateRoot<User<OrganizationUserId>, OrganizationUserId>> CreateAsync()
    {
        return Task.FromResult(this as IUserAggregateRoot<User<OrganizationUserId>, OrganizationUserId>);
    }

    /// <inheritdoc />
    public new OrganizationUser ToEntity()
    {
        return this;
    }
}
