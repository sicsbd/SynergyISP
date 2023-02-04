
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Helpers;

namespace SynergyISP.Domain.ValueObjects;
public readonly record struct UserName : IValueObject, IEquatable<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserName"/> class.
    /// </summary>
    public UserName()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserName"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    [JsonConstructor]
    public UserName(string name)
    {
        Value = name;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>
    /// News the.
    /// </summary>
    /// <returns>An IValueObject.</returns>
    public static new IValueObject New()
    {
        return new UserName(string.Empty);
    }

    public static implicit operator UserName(string value) => new() { Value = value };

    public static implicit operator string(UserName other) => other.Value;

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool operator ==(UserName value1, string value2)
    {
        return value1.Value.Equals(value2);
    }

    public static bool operator !=(UserName value1, string value2)
    {
        return !value1.Value.Equals(value2);
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
    public bool Equals([NotNullWhen(true)] string? other)
    {
        return !(other?.IsNullOrWhiteSpace() ?? false)
            && Value.Equals(other);
    }
}
