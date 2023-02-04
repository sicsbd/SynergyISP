
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Newtonsoft.Json;

namespace SynergyISP.Domain.Abstractions;
public record class Id
    : IValueObject, IEquatable<Guid>, IComparable<Id>, IComparable<Guid>, ICloneable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Id"/> class.
    /// </summary>
    public Id()
    {
        Value = Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Id"/> class.
    /// Prevents a default instance of the <see cref="Id"/> class from being created.
    /// </summary>
    /// <param name="value">The value.</param>
    [JsonConstructor]
    public Id(Guid value) => Value = value;

    /// <summary>
    /// Gets or inits the Id value.
    /// </summary>
    public Guid Value { get; init; }

    /// <inheritdoc/>
    public int CompareTo([NotNullWhen(true)] Id? other)
        => Value.CompareTo(other!.Value);

    /// <inheritdoc />
    public int CompareTo(Guid other) => Value.CompareTo(other);

    /// <inheritdoc />
    public bool Equals(Guid other) => Value.Equals(other);

    /// <summary>
    /// Clones the.
    /// </summary>
    /// <returns>An object.</returns>
    object ICloneable.Clone()
    {
        string json = JsonConvert.SerializeObject(this);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        using MemoryStream jsonStream = new(jsonBytes);
        byte[] newJsonBytes = jsonStream.ToArray();
        string newJson = Encoding.UTF8.GetString(newJsonBytes);
        var obj = JsonConvert.DeserializeObject<Id>(newJson);
        return obj!;
    }

    /// <inheritdoc />
    public static IValueObject New()
    {
        return new Id(Guid.NewGuid());
    }

    public static implicit operator Guid(Id id) => id.Value;

    public static implicit operator Id(Guid id) => new(id);

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }
}
