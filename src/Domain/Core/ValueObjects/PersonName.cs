using SynergyISP.Domain.Abstractions;

namespace SynergyISP.Domain.ValueObjects;
public record class PersonName : IValueObject, IEquatable<PersonName>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FullName"/> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    public PersonName(Name firstName, Name lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public Name FirstName { get; private init; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public Name LastName { get; private init; }

    /// <summary>
    /// Gets the nick name.
    /// </summary>
    public Name? NickName { get; private init; }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public Name? DisplayName { get; private init; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    public Name FullName => FirstName + LastName;

    /// <inheritdoc/>
    public static IValueObject New()
    {
        return new PersonName(string.Empty, string.Empty);
    }

    public static implicit operator string(PersonName name) => name.FullName.Trim();

    public static implicit operator PersonName(string name)
    {
        string[] splittedNames = name.Split(' ');
        if (splittedNames.Length == 2)
        {
            return new PersonName(splittedNames[0].Trim(), splittedNames[1].Trim());
        }

        var joinedFirstName = string.Join(' ', splittedNames.SkipLast(1));
        var lastName = splittedNames.Last();
        return new PersonName(joinedFirstName, lastName);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return FullName.ToString();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return FullName.GetHashCode();
    }

    /// <summary>
    /// Equals the.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>true if two objects are equal.</returns>
    public virtual bool Equals(PersonName? other)
        => other is not null
        && FirstName.Equals(other.FirstName)
        && LastName.Equals(other.LastName);
}
