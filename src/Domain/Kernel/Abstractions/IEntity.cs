namespace SynergyISP.Domain.Abstractions;
using System;
using Newtonsoft.Json;
using System.Text;

/// <summary>
/// Represents a domain entity.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IEntity<out TKey> : ICloneable
    where TKey : Id
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    public TKey Id { get; }

    /// <inheritdoc/>
    object ICloneable.Clone()
    {
        string json = JsonConvert.SerializeObject(this);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        using MemoryStream jsonStream = new (jsonBytes);
        byte[] newJsonBytes = jsonStream.ToArray();
        string newJson = Encoding.UTF8.GetString(newJsonBytes);
        var obj = JsonConvert.DeserializeObject<IEntity<TKey>>(newJson);
        return obj!;
    }
}
