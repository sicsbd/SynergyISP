namespace SynergyISP.Domain.ValueObjects;
using Abstractions;

public record class Name : IValueObject
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    protected string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Name"/> class.
    /// Prevents a default instance of the <see cref="UserName"/> class from being created.
    /// </summary>
    protected Name()
    {
        Value = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Name"/> class.
    /// Prevents a default instance of the <see cref="UserName"/> class from being created.
    /// </summary>
    /// <param name="name">name.</param>
    protected Name(string name)
    {
        Value = name;
    }

    /// <summary>
    /// News the.
    /// </summary>
    /// <returns>An IValueObject.</returns>
    public static IValueObject New()
    {
        return new Name() { Value = string.Empty };
    }

    public static implicit operator Name(string value) => new () { Value = value };

    public static implicit operator string(Name value) => value.Value;

    public static Name operator +(Name value1, Name value2)
    {
        return new Name($"{value1.Trim()} {value2.Trim()}");
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Trims the name.
    /// </summary>
    /// <returns>A Name.</returns>
    public Name Trim()
    {
        return new Name(Value.Trim());
    }
}
