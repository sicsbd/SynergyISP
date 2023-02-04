
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using SynergyISP.Domain.Abstractions;

namespace SynergyISP.Domain.ValueObjects;
/// <summary>
/// Represents a UserId.
/// </summary>
public record class TenantUserId
    : UserId, IValueObject, IComparable<Guid>, IComparable<string>, IMap<Id>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUserId"/> class.
    /// </summary>
    [JsonConstructor]
    public TenantUserId()
        : base()
    {
        Value = Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUserId"/> class.
    /// Prevents a default instance of the <see cref="TenantUserId"/> class from being created.
    /// </summary>
    /// <param name="original">The original.</param>
    [JsonConstructor]
    public TenantUserId(Guid original)
    {
        Value = original;
    }

    /// <inheritdoc />
    public static IValueObject New()
    {
        return new TenantUserId(Guid.NewGuid());
    }

    /// <inheritdoc />
    public int CompareTo(UserId? other) => other == null
            ? 0
            : Value.CompareTo(other.Value);

    public static implicit operator Guid(TenantUserId id) => id.Value;
    public static implicit operator string(TenantUserId id) => id.Value.ToString();

    public static implicit operator TenantUserId(Guid id) => new(id);
    public static implicit operator TenantUserId(string id) => new(Guid.Parse(id));

    public static bool operator ==(Guid id, TenantUserId id2) => id2.Value.Equals(id);
    public static bool operator !=(Guid id, TenantUserId id2) => !id2.Value.Equals(id);

    public static bool operator ==(TenantUserId id, Guid id2) => id.Value.Equals(id2);
    public static bool operator !=(TenantUserId id, Guid id2) => !id.Value.Equals(id2);

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
