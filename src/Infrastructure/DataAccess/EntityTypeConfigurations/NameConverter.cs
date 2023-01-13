namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
internal class NameConverter : ValueConverter<Name, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NameConverter"/> class.
    /// </summary>
    public NameConverter()
        : base(
            v => v.Value,
            v => new Name(v),
            new ConverterMappingHints(500, unicode: true))
    {
    }
}

internal class NameComparer : ValueComparer<Name>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NameComparer"/> class.
    /// </summary>
    public NameComparer()
        : base(
            (v1, v2) => v1!.Equals(v2),
            v => v.GetHashCode())
    {
    }
}
