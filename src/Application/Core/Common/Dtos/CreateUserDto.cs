namespace SynergyISP.Application.Common.Dtos;
public abstract record class CreateUserDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserDto"/> class.
    /// </summary>
    protected CreateUserDto()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserDto"/> class.
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

    /// <summary>
    /// Gets the id.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Gets or inits the user name.
    /// </summary>
    public string UserName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or inits the first name.
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or inits the last name.
    /// </summary>
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or inits the display name.
    /// </summary>
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or inits the nick name.
    /// </summary>
    public string NickName { get; init; } = string.Empty;
}
