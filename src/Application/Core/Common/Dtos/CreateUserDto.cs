namespace SynergyISP.Application.Common.Dtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using SynergyISP.Domain.Aggregates;

public record class CreateUserDto
    : IMapTo<User<UserId>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserDto"/> class.
    /// </summary>
    public CreateUserDto()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserDto"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="nickName">The nick name.</param>
    public CreateUserDto(
        string id,
        string userName,
        string firstName,
        string lastName,
        string displayName,
        string nickName)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        DisplayName = displayName;
        NickName = nickName;
    }

    public string Id { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string DisplayName { get; init; }
    public string NickName { get; init; }

    /// <inheritdoc/>
    void IMapTo<User<UserId>>.Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserDto, User<UserId>>()
            .ConvertUsing(u
                => new User<UserId>(u.Id.ToString())
                    .ChangeAccount(u.UserName, u.FirstName, u.LastName, u.DisplayName, u.NickName, "abc123@", u.Id, new UserProfileAggregate(u.FirstName)
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        DisplayName = u.DisplayName,
                        NickName = u.NickName,
                    })
                .ToEntity());
    }
}
