
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using SynergyISP.Domain.Abstractions;

namespace SynergyISP.Domain.ValueObjects;
/// <summary>
/// Represents a UserId.
/// </summary>
public record class CustomerId
    : UserId, IValueObject, IComparable<Guid>, IComparable<string>, IMap<Id>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerId"/> class.
    /// </summary>
    [JsonConstructor]
    public CustomerId()
        : base()
    {
        Value = Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerId"/> class.
    /// </summary>
    /// <param name="original">The original.</param>
    public CustomerId(Guid original)
    {
        Value = original;
    }

    /// <inheritdoc />
    public static IValueObject New()
    {
        return new CustomerId(Guid.NewGuid());
    }

    /// <inheritdoc />
    public int CompareTo(CustomerId? other) => other == null
            ? 0
            : Value.CompareTo(other.Value);

    public static implicit operator Guid(CustomerId id) => id.Value;
    public static implicit operator string(CustomerId id) => id.Value.ToString();

    public static implicit operator CustomerId(Guid id) => new(id);
    public static implicit operator CustomerId(string id) => new(Guid.Parse(id));

    public static bool operator ==(Guid id, CustomerId id2) => id2.Value.Equals(id);
    public static bool operator !=(Guid id, CustomerId id2) => !id2.Value.Equals(id);

    public static bool operator ==(CustomerId id, Guid id2) => id.Value.Equals(id2);
    public static bool operator !=(CustomerId id, Guid id2) => !id.Value.Equals(id2);

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
