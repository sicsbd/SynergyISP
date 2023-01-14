namespace SynergyISP.Application.Common.Dtos;
using Domain.Abstractions;
using Domain.Entities;

public sealed record class TenantUserDto
    : UserDto, IMapFrom<TenantUser>, IMapTo<TenantUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUserDto"/> class.
    /// </summary>
    public TenantUserDto()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUserDto"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="nickName">The nick name.</param>
    public TenantUserDto(
        string id,
        string userName,
        string firstName,
        string? lastName,
        string? displayName,
        string? nickName)
        : base(id, userName, firstName, lastName, displayName, nickName)
    {
    }
}
