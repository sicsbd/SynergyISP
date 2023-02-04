
using AutoMapper;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Application.Common.Dtos;
public sealed record class CustomerDto
    : UserDto, IMapFrom<Customer>, IMapTo<Customer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerDto"/> class.
    /// </summary>
    public CustomerDto()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerDto"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="nickName">The nick name.</param>
    public CustomerDto(
        string id,
        string userName,
        string firstName,
        string? lastName,
        string? displayName,
        string? nickName)
        : base(id, userName, firstName, lastName, displayName, nickName)
    {
    }

    public string FullName { get; private init; }

    public static implicit operator CustomerDto(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.UserName,
            customer.FirstName,
            customer.LastName,
            customer.DisplayName,
            customer.NickName);
    }

    /// <inheritdoc/>
    void IMapFrom<Customer>.Mapping(Profile profile)
    {
        profile
            .CreateMap<Customer, CustomerDto>()
            .ForMember(c => c.FullName, opt => opt.MapFrom(src => src.FirstName + src.LastName));
    }
}
