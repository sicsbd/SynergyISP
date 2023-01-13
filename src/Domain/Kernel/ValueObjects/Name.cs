namespace SynergyISP.Domain.ValueObjects;
using Abstractions;
using Newtonsoft.Json;
using SynergyISP.Domain.Helpers;

public readonly record struct Name : IValueObject, IEquatable<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Name"/> class.
    /// Prevents a default instance of the <see cref="UserName"/> class from being created.
    /// </summary>
    public Name()
    {
        Value = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Name"/> class.
    /// Prevents a default instance of the <see cref="UserName"/> class from being created.
    /// </summary>
    /// <param name="name">name.</param>
    [JsonConstructor]
    public Name(string name)
    {
        Value = name;
    }

    /// <summary>
    /// Gets or inits the value.
    /// </summary>
    public string Value { get; init; }

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
        return new Name($"{value1.Trim()} {value2.Trim()}".Trim());
    }

    public static bool operator ==(Name value1, string value2)
    {
        return value1.Value.Equals(value2);
    }

    public static bool operator !=(Name value1, string value2)
    {
        return !value1.Value.Equals(value2);
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

    /// <inheritdoc/>
    public bool Equals(string? other)
    {
        return !(other?.IsNullOrWhiteSpace() ?? false)
            && Value.Equals(other);
    }
}
