namespace SynergyISP.Domain.ValueObjects;

using System.Diagnostics.CodeAnalysis;
using Abstractions;
using Newtonsoft.Json;

/// <summary>
/// Represents a UserId.
/// </summary>
public record class UserId
    : Id, IValueObject, IComparable<Guid>, IComparable<string>, IMap<Id>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </summary>
    [JsonConstructor]
    public UserId()
        : base()
    {
        Value = Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// Prevents a default instance of the <see cref="UserId"/> class from being created.
    /// </summary>
    /// <param name="original">The original.</param>
    [JsonConstructor]
    public UserId(Guid original)
    {
        Value = original;
    }

    /// <inheritdoc />
    public static IValueObject New()
    {
        return new UserId(Guid.NewGuid());
    }

    /// <inheritdoc />
    public int CompareTo(UserId? other) => other == null
            ? 0
            : Value.CompareTo(other.Value);

    public static implicit operator Guid(UserId id) => id.Value;
    public static implicit operator string(UserId id) => id.Value.ToString();

    public static implicit operator UserId(Guid id) => new(id);
    public static implicit operator UserId(string id) => new(Guid.Parse(id));

    public static bool operator ==(Guid id, UserId id2) => id2.Value.Equals(id);
    public static bool operator !=(Guid id, UserId id2) => !id2.Value.Equals(id);

    public static bool operator ==(UserId id, Guid id2) => id.Value.Equals(id);
    public static bool operator !=(UserId id, Guid id2) => !id.Value.Equals(id);

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }

    /// <inheritdoc />
    int IComparable<Guid>.CompareTo(Guid other) => Value.CompareTo(other);

    /// <inheritdoc />
    int IComparable<string>.CompareTo([NotNullWhen(true)] string? other) => Value.CompareTo(Guid.Parse(other!));
}
