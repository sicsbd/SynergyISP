namespace SynergyISP.Application.Common.Dtos;
using Domain.Abstractions;
using Domain.Entities;

public sealed record class OrganizationUserDto
    : UserDto, IMapFrom<OrganizationUser>, IMapTo<OrganizationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUserDto"/> class.
    /// </summary>
    public OrganizationUserDto()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUserDto"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="nickName">The nick name.</param>
    public OrganizationUserDto(
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
