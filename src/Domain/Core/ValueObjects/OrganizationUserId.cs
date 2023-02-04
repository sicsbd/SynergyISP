
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using SynergyISP.Domain.Abstractions;

namespace SynergyISP.Domain.ValueObjects;
/// <summary>
/// Represents a UserId.
/// </summary>
public record class OrganizationUserId
    : UserId, IValueObject, IComparable<Guid>, IComparable<string>, IMap<Id>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUserId"/> class.
    /// </summary>
    [JsonConstructor]
    public OrganizationUserId()
        : base()
    {
        Value = Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUserId"/> class.
    /// Prevents a default instance of the <see cref="OrganizationUserId"/> class from being created.
    /// </summary>
    /// <param name="original">The original.</param>
    public OrganizationUserId(Guid original)
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

    public static implicit operator Guid(OrganizationUserId id) => id.Value;
    public static implicit operator string(OrganizationUserId id) => id.Value.ToString();

    public static implicit operator OrganizationUserId(Guid id) => new(id);
    public static implicit operator OrganizationUserId(string id) => new(Guid.Parse(id));

    public static bool operator ==(Guid id, OrganizationUserId id2) => id2.Value.Equals(id);
    public static bool operator !=(Guid id, OrganizationUserId id2) => !id2.Value.Equals(id);

    public static bool operator ==(OrganizationUserId id, Guid id2) => id.Value.Equals(id2);
    public static bool operator !=(OrganizationUserId id, Guid id2) => !id.Value.Equals(id2);

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
