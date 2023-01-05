namespace SynergyISP.Domain.ValueObjects;
using Abstractions;

public record class UserName : Name
{
    /// <summary>
    /// Prevents a default instance of the <see cref="UserName"/> class from being created.
    /// </summary>
    private UserName()
        : base()
    {
    }

    /// <summary>
    /// News the.
    /// </summary>
    /// <returns>An IValueObject.</returns>
    public static new IValueObject New()
    {
        // unary binary
        return new UserName();
    }

    public static implicit operator UserName(string value) => new () { Value = value };

    public static implicit operator string(UserName other) => other.Value;

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }
}
