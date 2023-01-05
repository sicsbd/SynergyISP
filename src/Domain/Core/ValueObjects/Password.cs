namespace SynergyISP.Domain.ValueObjects;

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Abstractions;
using Helpers;

public record class Password : IPassword<Password>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="password">The password.</param>
    protected Password(string password)
    {
        Value = password.ComputeHash(SHA512.Create(), Encoding.UTF8);
    }

    /// <summary>
    /// Gets or inits the value.
    /// </summary>
    protected string Value { get; init; }

    /// <summary>
    /// Creates a new instance on Password.
    /// </summary>
    /// <returns>A IValueObject.</returns>
    public static IValueObject New()
    {
        return new Password(string.Empty);
    }

    public static implicit operator Password(string password)
    {
        return new Password(password);
    }

    public static bool operator ==(Password password, string otherPassword)
    {
        return (password as IEquatable<string>).Equals(otherPassword);
    }

    public static bool operator !=(Password password, string otherPassword)
    {
        return !(password as IEquatable<string>).Equals(otherPassword);
    }

    /// <inheritdoc/>
    public bool Equals([NotNullWhen(true)] string? value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && Value.Equals(value.ComputeHash(SHA512.Create(), Encoding.UTF8));
    }
}
