namespace SynergyISP.Domain.ValueObjects;
using Abstractions;

/// <summary>
/// Represents a UserId.
/// </summary>
public record class UserId
    : Id, IEquatable<UserId>, IComparable<UserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// Prevents a default instance of the <see cref="UserId"/> class from being created.
    /// </summary>
    /// <param name="original">The original.</param>
    protected UserId(Guid original)
        : base(original)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </summary>
    /// <param name="original">The original.</param>
    protected UserId(Id original)
        : this((Guid)original)
    {
    }

    /// <inheritdoc />
    public int CompareTo(UserId? other) => other == null
            ? 0
            : Value.CompareTo(other.Value);

    public static implicit operator Guid(UserId id) => id.Value;

    public static implicit operator UserId(Guid id) => new (id);
}
