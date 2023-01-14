namespace SynergyISP.Application.Common.Dtos;
public abstract record class UserDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserDto"/> class.
    /// </summary>
    protected UserDto()
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
    public UserDto(
        string id,
        string userName,
        string firstName,
        string? lastName,
        string? displayName,
        string? nickName)
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
    public string? LastName { get; init; }
    public string? DisplayName { get; init; }
    public string? NickName { get; init; }

    public string FullName { get => $"{FirstName} {LastName}"; }
}
