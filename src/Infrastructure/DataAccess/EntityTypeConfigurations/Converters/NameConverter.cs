using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations.Converters;
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
